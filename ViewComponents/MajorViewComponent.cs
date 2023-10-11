﻿using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.ViewComponents
{
	public class MajorViewComponent : ViewComponent
	{
		SchoolContext db;
		List<Major> majors;

		public MajorViewComponent(SchoolContext context)
		{
			db = context;
			majors = db.Majors.ToList();
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("RenderMajor", majors);
		}
	}
}
