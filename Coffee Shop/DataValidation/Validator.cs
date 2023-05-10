using System;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DataValidation
{
    public class Validator
    {
        public Validator(object instance)
        {
            this.Instance = instance;
        }

        private object Instance;

        public enum ValidationBased
        {
            Email,
            Password,
            TextTo, // gsgsaaaaaaaaaaaaaaaaaa
            OrdinaryDigits,
            Links,
            BankCVV,
            BankPeriod,
            BankNumber,
            Image,
            Date
        }

        public bool Verify(ValidationBased validationBased, string value, string property)
        {
            string regex = string.Empty;
            string errorMessage = string.Empty;

            switch (validationBased)
            {
                case ValidationBased.Email:
                    regex = "^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[;]{0,1}\\s*)+$";
                    errorMessage = "Incorrect mail";
                    break;
                case ValidationBased.Password:
                    regex = @"^.{6,}$";
                    errorMessage = "Incorrect password";
                    break;
                case ValidationBased.TextTo:
                    regex = @"^[a-zA-Z\s]{5,}$"; //@"^[a-zA-Z]{5,}$";
                    errorMessage = "Incorrect data";
                    break;
                case ValidationBased.OrdinaryDigits:
                    regex = @"^[1-9]\d{0,2}$";
                    errorMessage = "Incorrect data";
                    break;
                case ValidationBased.Links:
                    regex = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
                    errorMessage = "Incorrect link";
                    break;
                case ValidationBased.BankCVV:
                    regex = @"^[0-9]{1,3}$";
                    errorMessage = "Incorrect CVV";
                    break;
                case ValidationBased.BankPeriod:
                    regex = @"^(0?[1-9]|1[0-2])\/[0-9]{2}$";
                    errorMessage = "Incorrect period";
                    break;
                case ValidationBased.BankNumber:
                    regex = @"^\d{4}-\d{4}-\d{4}-\d{4}$";
                    errorMessage = "Incorrect number";
                    break;
                case ValidationBased.Image:
                    regex = @"^([a-zA-Z]:)?\\(\w+\\)+\w+(\.\w+)?$";
                    errorMessage = "Incorrect path";
                    break;
                case ValidationBased.Date:
                    regex = @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.(19|20)\d\d$";
                    errorMessage = "Incorrect date";
                    break;
            }
            return Validate(regex, errorMessage, value, property);
        }

        public bool Validate(string regex, string errorMessage, string value, string property)
        {
            Regex reg = new Regex(regex);
            if (!reg.IsMatch(value))
            {
                SetPropertyValue(Instance, property, errorMessage);
                return false;
            }
            SetPropertyValue(Instance, property, "");
            return true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        //public bool ValidationOfEmail(string value, string property)
        //{
        //    Regex regex = new Regex("^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[;]{0,1}\\s*)+$");
        //    if (!regex.IsMatch(value) || value.Length < 10)
        //    {
        //        SetPropertyValue(Instance, property, "Incorrect email");
        //        return false;
        //    }
        //    SetPropertyValue(Instance, property, "");
        //    return true;
        //}

        //public bool CheckingForPasswordLength(string value, string property)
        //{
        //    if (value.Length < 6)
        //    {
        //        SetPropertyValue(Instance, property, "The password is too short");
        //        return false;
        //    }
        //    SetPropertyValue(Instance, property, "");
        //    return true;
        //}

        //public bool ValidationOfPlainText(string value, string property)
        //{
        //    Regex regex = new Regex("^[a-zA-Z]+$");
        //    if (!regex.IsMatch(value) || value.Length < 5)
        //    {
        //        SetPropertyValue(Instance, property, "Incorrect data");
        //        return false;
        //    }
        //    SetPropertyValue(Instance, property, "");
        //    return true;
        //}

        //public bool ValidationOfOrdinaryDigits(string value, string property)
        //{
        //    Regex regex = new Regex(@"^[1-9]\d{0,2}$");
        //    if (!regex.IsMatch(value))
        //    {
        //        SetPropertyValue(Instance, property, "Incorrect data");
        //        return false;
        //    }
        //    SetPropertyValue(Instance, property, "");
        //    return true;
        //}

        //public bool CheckingTheCorrectnessOfLinks(string value, string property)
        //{
        //    string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
        //    Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //    if (!regex.IsMatch(value))
        //    {
        //        SetPropertyValue(Instance, property, "Incorrect link");
        //        return false;
        //    }
        //    SetPropertyValue(Instance, property, "");
        //    return true;
        //}


        private static void SetPropertyValue(object obj, string propertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            if (property != null)
            {
                property.SetValue(obj, value, null);
            }
            else
            {
                throw new ArgumentException($"Свойство {propertyName} не найдено в типе {obj.GetType()}");
            }
        }
    }
}
