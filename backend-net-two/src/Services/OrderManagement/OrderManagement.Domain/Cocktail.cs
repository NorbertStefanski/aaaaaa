using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class Cocktail : Entity
    {
        private string id;
        public string Id
        {
            get => id;
            set
            {
                if (IsValidEan13(value))
                {
                    id = value;
                }
                else
                {
                    throw new ArgumentException("Invalid EAN-13 code");
                }
            }
        }
        
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        //EF
        public Cocktail()
        {

        }
        public Cocktail(string id, string name, string imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }

        private bool IsValidEan13(string ean13)
        {
            // Check that the input is a 13-digit string
            if (ean13.Length != 13 || !long.TryParse(ean13, out _))
            {
                return false;
            }

            // Calculate the check digit
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = ean13[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }
            int checkDigit = 10 - (sum % 10);
            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            // Compare the calculated check digit to the one in the EAN-13 code
            return checkDigit == ean13[12] - '0';
        }
    }
}
