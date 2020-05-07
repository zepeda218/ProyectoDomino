namespace ProyectoDomino
{
    //Declaracion de la clase
    public class Ficha
    {
       
        public int primerNumero { get; set; }
        public int segundoNumero { get; set; }
        public bool esDoble { get; set; }

        public int ObtenerPuntaje() {
            return primerNumero + segundoNumero;
        }

        public int ObtenerMayorNumero()
        {
            return primerNumero > segundoNumero ? primerNumero : segundoNumero;
        }

        public override string ToString()
        {
            return $"|{primerNumero}:{segundoNumero}|";
        }
    }
   
}
