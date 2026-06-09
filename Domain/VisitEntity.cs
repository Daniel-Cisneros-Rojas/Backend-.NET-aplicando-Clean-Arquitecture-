using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class VisitEntity
    {
        public Guid Id { get; private set; }
        public Guid PersonId { get; private set; }
        public DateTime EntryTime { get; private set; }
        public DateTime? ExitTime { get; private set; }

        public personEntity? Person { get; private set; }

        public bool isActive => ExitTime == null;

        public TimeSpan? Duration => ExitTime.HasValue ? ExitTime.Value - EntryTime : null;

        public VisitEntity(Guid personId,DateTime? entryTime=null)
        {
            if(personId==Guid.Empty)
            {
                throw new ArgumentException("El id de la persona esta vacio",nameof(personId));
            }

            Id = Guid.NewGuid();
            PersonId = personId;
            EntryTime = entryTime ?? DateTime.UtcNow; // el doble ?? significa que si lo del lado izquierdo es null se asigna lo del derecho
            ExitTime = null;
        }

        public void RegisterExit(DateTime? exitTime=null)
        {
            var exit = exitTime ?? DateTime.UtcNow;

            if (ExitTime.HasValue)
            {
                throw new InvalidOperationException("Esta visita ya tiene una salida registrada");
            }

            if (exit <= EntryTime)
            {
                throw new ArgumentException("La salida debe ser despues de la entrada",nameof(exitTime));
            }

            ExitTime = exit;
        }
    }
}
