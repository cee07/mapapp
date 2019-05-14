﻿using System;
using MapKit;

namespace mapapp.iOS {
	public class CustomMKAnnotationView : MKAnnotationView{

		public CustomMKAnnotationView (IMKAnnotation annotation, string id) : base(annotation, id) { }

		public string ID { get; set; }

	}
}