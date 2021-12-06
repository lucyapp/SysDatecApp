using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScanApp.Models
{
    public class Post
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
    }
    public class ArchivosRecientes
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Fecha { get; set; }


    }
    public class NombresCarpetas
    {
        //NombresCarpetas
        public string Name { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Picture { get; set; }
        public string CantidadArchivos { get; set; }
    }
    public class NombresubCarpetas
    {
        //NombresCarpetas
        public string Name { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Picture { get; set; }
        public string CantidadArchivos { get; set; }
        public string DirectorioPadre { get; set; }
    }
    public class ListaCompletaMediaDirectoy {
        public ObservableCollection<ListaCompletaMediaDirectoy> ListaAllCarpetas { get; set; }
    }
    public class ListadosMedia
    {
        //NombresCarpetas
        public ObservableCollection<NombresCarpetas> Carpetas { get; set; }
        public ObservableCollection<NombresubCarpetas> Subcarpetas { get; set; }
       // public ObservableCollection<string> ListaAllCarpetasSubcarpetas { get; set; }
    }
    public class responseLogin { public int result { get; set; } }
    public class OnboardingModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public bool Opcion0 { get; set; }
        public bool Opcion1 { get; set; }
    }
    public class PdfDocEntity
    {
        public string FileName { get; set; }
        public string Url { get; set; }
    }
    public class ListaArchivosCarpetas
    {
        public string Nombre { get; set; }
        public string Carpeta { get; set; }
        public string NombreDirectorio { get; set; }
    }

}