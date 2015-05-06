using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace SozialWebApplication.Models
{
	public class Group
	{
		public int Id { get; set; }
		public string GroupName { get; set; }
	}
}