using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace InventorAccessPortal.Web.Models
{
    public class _ToasterModel
    {
        private List<String> _Errors = new List<String>();
        private List<String> _Warnings = new List<String>();
        private List<String> _Successes = new List<String>();
        public List<String> _IgnoredWarnings { get; set; } = new List<String>();

        public void AddError(String err)
        {
            _Errors.Add(err);
        }

        public void AddError(Enum err) {
            _Errors.Add(GetEnumDescription(err));
        }

        public void AddErrors(List<Enum> errs)
        {
            foreach (var err in errs)
                _Errors.Add(GetEnumDescription(err));
        }

        public List<String> GetErrors()
        {
            return _Errors.Distinct().ToList();
        }

        public void AddWarning(Enum warn)
        {
            _Warnings.Add(GetEnumDescription(warn));
        }

        public void AddWarnings(List<Enum> warns)
        {
            foreach (var warn in warns)
                _Warnings.Add(GetEnumDescription(warn));
        }

        public List<String> GetWarnings()
        {
            return _Warnings.Distinct().ToList();
        }

        public void AddSuccess(Enum success)
        {
            _Successes.Add(GetEnumDescription(success));
        }

        public void AddSuccesses(List<Enum> successes)
        {
            foreach (var success in successes)
                _Successes.Add(GetEnumDescription(success));
        }

        public List<String> GetSuccesses()
        {
            return _Successes.Distinct().ToList();
        }

        public void ClearToaster()
        {
            _Errors = new List<String>();
            _Warnings = new List<String>();
            _Successes = new List<String>();
        }

        public bool HasWarnings() {
            return _Warnings.Any(p => !_IgnoredWarnings.Contains(p));
        }

        public void IgnoreWarnings()
        {
            _IgnoredWarnings = new List<String>(_Warnings.Select(p => String.Copy(p)));
        }

        private static string GetEnumDescription(Enum error)
        {
            FieldInfo fi = error.GetType().GetField(error.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return "An unknown Error/Warning occured.";
        }
    }
}