using System.Text.RegularExpressions;

namespace Domain
{
    public class personEntity
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = string.Empty;

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } =string.Empty;

        public string Email { get; private set; } =string.Empty;

        public string PhoneNumber { get; private set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
        public personEntity(string code, string firstName,string lastName,string email, string phoneNumber) 
        { 
            ValidarCode(code);
            ValidarFirstName(firstName);
            ValidarLastName(lastName);
            ValidarEmail(email);
            ValidarPhoneNumber(phoneNumber);
            Id= Guid.NewGuid();
            Code = code.Trim().ToUpper();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim().ToLower();
            PhoneNumber = phoneNumber.Trim();

        }

        public void UpdatePersonalInfo(string firstName, string lastName, string email, string phoneNumber)
        {
            ValidarFirstName(firstName);
            ValidarLastName(lastName);
            ValidarEmail(email);
            ValidarPhoneNumber(phoneNumber);

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim().ToLower();
            PhoneNumber = phoneNumber.Trim();

        }

        private void ValidarCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("El codigo no puede estar vacio", nameof(code));
            }

            if (code.Trim().Length < 3)
            {
                throw new ArgumentException("El codigo no puede tener menos de 3 caracteres", nameof(code));
            }

            if (code.Trim().Length > 20)
            {
                throw new ArgumentException("El codigo no puede tener mas de 20 caracteres", nameof(code));
            }
        }

        private void ValidarFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("El nombre no puede estar vacio", nameof(firstName));
            }

            if (firstName.Trim().Length < 3)
            {
                throw new ArgumentException("El nombre no puede tener menos de 3 caracteres", nameof(firstName));
            }

            if (firstName.Trim().Length > 50)
            {
                throw new ArgumentException("El nombre no puede tener mas de 50 caracteres", nameof(firstName));
            }
        }

        private void ValidarLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("El apellido no puede estar vacio", nameof(lastName));
            }

            if (lastName.Trim().Length < 3)
            {
                throw new ArgumentException("El apellido no puede tener menos de 3 caracteres", nameof(lastName));
            }

            if (lastName.Trim().Length > 50)
            {
                throw new ArgumentException("El apellido no puede tener mas de 50 caracteres", nameof(lastName));
            }
        }

        private void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("El email no puede estar vacio", nameof(email));
            }

            if (email.Trim().Length < 3)
            {
                throw new ArgumentException("El email no puede tener menos de 3 caracteres", nameof(email));
            }

            if (email.Trim().Length > 100)
            {
                throw new ArgumentException("El email no puede tener mas de 100 caracteres", nameof(email));
            }

            var emailPattern=@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if(!Regex.IsMatch(email,emailPattern))
            {
                throw new ArgumentException("El email no tiene un formato valido", nameof(email));
            }
        }

        private void ValidarPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("El telefono no puede estar vacio", nameof(phoneNumber));
            }

            if (phoneNumber.Trim().Length < 3)
            {
                throw new ArgumentException("El telefono no puede tener menos de 3 caracteres", nameof(phoneNumber));
            }

            if (phoneNumber.Trim().Length > 20)
            {
                throw new ArgumentException("El telefono no puede tener mas de 20 caracteres", nameof(phoneNumber));
            }
        }

         

    }
}
