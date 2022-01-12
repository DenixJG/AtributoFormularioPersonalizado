using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AtributoFormularioPersonalizado.MisControles
{
    /// <summary>
    /// Lógica de interacción para InputPersonalizado.xaml
    /// </summary>
    public partial class InputPersonalizado : UserControl
    {
        public string TipoValidacion { get; set; }
        private const string LETRAS_DNI = "TRWAGMYFPDXBNJZSQVHLCKE";
        public InputPersonalizado()
        {
            InitializeComponent();
        }

        private void TxtDatos_LostFocus(object sender, RoutedEventArgs e)
        {

            // Valida un Codigo Postal
            if (TipoValidacion == "CP" || TipoValidacion == "cp")
            {
                if (TxtDatos.Text.Length != 5)
                {
                    LblAlerta.Content = "El CP debe contener 5 digitos.";
                }
                else
                {
                    try
                    {
                        int.Parse(TxtDatos.Text);
                        LblAlerta.Content = "CP correcto";
                    }
                    catch (FormatException)
                    {
                        LblAlerta.Content = "CP No puede tener letras";
                    }
                }
            }

            // Valida el Telefono
            if (TipoValidacion == "Tel" || TipoValidacion == "Tlf" || TipoValidacion == "tel" || TipoValidacion == "tlf")
            {

                if (TxtDatos.Text.Length != 9)
                {
                    LblAlerta.Content = "El Tel debe contener 9 digitos.";
                }
                else
                {
                    try
                    {
                        int.Parse(TxtDatos.Text);
                        LblAlerta.Content = "Tel Correcto";
                    }
                    catch (FormatException)
                    {
                        LblAlerta.Content = "El Tel no puede tener letras.";
                    }
                }
            }

            // Validar el DNI
            if (TipoValidacion == "DNI" || TipoValidacion == "dni")
            {

                Match match = new Regex(@"\b(\d{8})\b").Match(TxtDatos.Text);
                if (match.Success)
                {
                    try
                    {
                        int dni = int.Parse(TxtDatos.Text);
                        // Calculamos la letra del DNI obteniendo el módulo de dividir entre 23 los
                        // 8 digitos del DNI.
                        TxtDatos.Text = dni + "" + LETRAS_DNI[dni % 23];
                        LblAlerta.Content = "DNI Correcto";
                    }
                    catch (FormatException)
                    {
                        LblAlerta.Content = "8 Primeros teben ser digitos";
                    }
                }
                else if (TxtDatos.Text.Length == 9)
                {
                    {
                        try
                        {
                            string letra = TxtDatos.Text.Substring(8, 1);
                            int.Parse(letra);
                            LblAlerta.Content = "DNI Incorrecto";
                        }
                        catch (FormatException)
                        {
                            LblAlerta.Content = "DNI Correcto";
                        }
                    }
                }
                else
                {
                    LblAlerta.Content = "El DNI debe tener menos de 9 carácteres.";
                }
            }
        }
    }
}
