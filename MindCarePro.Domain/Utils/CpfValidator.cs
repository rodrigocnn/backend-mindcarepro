
namespace MindCarePro.Domain.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class CpfValidator
    {
        public static bool Validate(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser vazio.");

            var numbersOnly = RemoveNonDigits(cpf);

            if (!HasValidLength(numbersOnly) || HasAllEqualDigits(numbersOnly))
                throw new ArgumentException("Formato do CPF inválido.");

            if (!HasValidCheckDigits(numbersOnly))
                throw new ArgumentException("CPF inválido.");

            return true;
        }

        private static string RemoveNonDigits(string input)
        {
            return Regex.Replace(input, @"\D", "");
        }

        private static bool HasValidLength(string cpf)
        {
            return cpf.Length == 11;
        }

        private static bool HasAllEqualDigits(string cpf)
        {
            return cpf.All(c => c == cpf[0]);
        }

        private static bool HasValidCheckDigits(string cpf)
        {
            var firstDigit = CalculateCheckDigit(cpf, 9);
            var secondDigit = CalculateCheckDigit(cpf, 10);

            return firstDigit == (cpf[9] - '0') &&
                   secondDigit == (cpf[10] - '0');
        }

        private static int CalculateCheckDigit(string cpf, int length)
        {
            var sum = 0;
            var weight = length + 1;

            for (int i = 0; i < length; i++)
            {
                sum += (cpf[i] - '0') * (weight - i);
            }

            var result = (sum * 10) % 11;
            return result == 10 ? 0 : result;
        }
    }

