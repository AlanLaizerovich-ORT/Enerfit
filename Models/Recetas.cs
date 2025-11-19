namespace Enerfit.Models;

public class Recetas {
 public int IdRecetas { get; set; }
 public string nombreReceta { get; set; }
 public int Calorias { get; set; }
 public int Proteinas { get; set; }
 public int Carbohidratos { get; set; }
public string Ingredientes { get; set; }

        public int IdIngredientes { get; set; }

}