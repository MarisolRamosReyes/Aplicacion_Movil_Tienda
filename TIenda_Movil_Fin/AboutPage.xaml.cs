using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.Globalization;

namespace TIenda_Movil_Fin
{
    public partial class AboutPage : ContentPage
    {
        private const string CreditLimitKey = "CreditLimit";
        private const string SalaryKey = "Salary";
        private const string ImageNameKey = "ImageName";
        private const string PdfNameKey = "PdfName";

        private int _creditLimit = 0;

        public AboutPage()
        {
            InitializeComponent();
            ResetFields(); // Reiniciar campos al inicializar
            LoadPersistentData();
            UpdateFieldVisibility();
        }

        private void ResetFields()
        {
            _creditLimit = 0;
            Preferences.Set(CreditLimitKey, _creditLimit); // Guardar el crédito en 0 en las preferencias

            SalaryEntry.Text = string.Empty; // Limpiar el campo de salario
            ImageNameLabel.Text = "No se ha seleccionado una imagen"; // Restablecer el texto de la imagen INE
            PdfNameLabel.Text = "No se ha seleccionado un archivo PDF"; // Restablecer el texto del comprobante de domicilio

            // Guardar los valores reiniciados en las preferencias
            Preferences.Set(SalaryKey, SalaryEntry.Text);
            Preferences.Set(ImageNameKey, ImageNameLabel.Text);
            Preferences.Set(PdfNameKey, PdfNameLabel.Text);
        }

        private void LoadPersistentData()
        {
            // Recuperar datos usando Preferences
            _creditLimit = Preferences.Get(CreditLimitKey, 0);
            SalaryEntry.Text = Preferences.Get(SalaryKey, string.Empty);
            ImageNameLabel.Text = Preferences.Get(ImageNameKey, "No se ha seleccionado una imagen");
            PdfNameLabel.Text = Preferences.Get(PdfNameKey, "No se ha seleccionado un archivo PDF");
        }

        private void SavePersistentData()
        {
            // Guardar datos usando Preferences
            Preferences.Set(CreditLimitKey, _creditLimit);
            Preferences.Set(SalaryKey, SalaryEntry.Text);
            Preferences.Set(ImageNameKey, ImageNameLabel.Text);
            Preferences.Set(PdfNameKey, PdfNameLabel.Text);
        }

        private async void OnIncreaseCreditClicked(object sender, EventArgs e)
        {
            // Validar campos requeridos según el crédito actual
            bool isSalaryValid = !string.IsNullOrWhiteSpace(SalaryEntry.Text);
            bool isImageValid = !string.IsNullOrWhiteSpace(ImageNameLabel.Text) && IsImage(ImageNameLabel.Text);
            bool isPdfValid = !string.IsNullOrWhiteSpace(PdfNameLabel.Text) && PdfNameLabel.Text.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
            bool isPhoneValid = IsPhoneNumberValid(PhoneEntry.Text);

            if (_creditLimit == 0) // Si el crédito es 0, validar también los datos personales
            {
                bool arePersonalFieldsValid = !string.IsNullOrWhiteSpace(NameEntry.Text) &&
                                              !string.IsNullOrWhiteSpace(LastNameEntry.Text) &&
                                              !string.IsNullOrWhiteSpace(MotherLastNameEntry.Text) &&
                                              isPhoneValid &&
                                              !string.IsNullOrWhiteSpace(AddressEntry.Text) &&
                                              !string.IsNullOrWhiteSpace(EmailEntry.Text) &&
                                              IsValidEmail(EmailEntry.Text); // Validar correo

                if (!arePersonalFieldsValid)
                {
                    if (!IsValidEmail(EmailEntry.Text))
                    {
                        await DisplayAlert("Error", "Por favor ingrese un correo electrónico válido.", "OK");
                    }
                    else if (!isPhoneValid)
                    {
                        await DisplayAlert("Error", "Por favor ingrese un número de teléfono válido de 10 dígitos.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Por favor complete todos los campos personales correctamente.", "OK");
                    }
                    return;
                }
            }

            if (isSalaryValid && isImageValid && isPdfValid)
            {
                _creditLimit += 3000; // Aumentar el crédito en 3000
                SavePersistentData(); // Guardar cambios persistentes
                await DisplayAlert("Éxito", $"El crédito ha aumentado en $3000. Crédito total: ${_creditLimit}", "OK");
                UpdateFieldVisibility(); // Actualizar visibilidad de campos
                PersonalDataFrame.IsVisible = false;
            }
            else
            {
                if (!isSalaryValid)
                {
                    await DisplayAlert("Error", "Por favor ingrese su salario mensual neto.", "OK");
                }
                else if (!isImageValid)
                {
                    await DisplayAlert("Error", "Por favor seleccione una imagen INE válida.", "OK");
                }
                else if (!isPdfValid)
                {
                    await DisplayAlert("Error", "Por favor seleccione un archivo PDF de comprobante de domicilio válido.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Por favor complete los campos requeridos (Salario, INE y Comprobante de domicilio).", "OK");
                }
            }
        }

        // Método para validar el correo electrónico
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Método para validar el número de teléfono
        private bool IsPhoneNumberValid(string phone)
        {
            return !string.IsNullOrWhiteSpace(phone) && phone.Length == 10 && phone.All(char.IsDigit);
        }

        // Método para validar el nombre de archivo de la imagen
        private bool IsImage(string fileName)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

        // Método para formatear el salario como moneda
        private void OnSalaryEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(SalaryEntry.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-MX"), out double value))
            {
                SalaryEntry.TextChanged -= OnSalaryEntryTextChanged;
                SalaryEntry.Text = string.Format(CultureInfo.GetCultureInfo("es-MX"), "{0:C0}", value); // Agregar el signo de pesos
                SalaryEntry.TextChanged += OnSalaryEntryTextChanged;
            }
        }

        private void UpdateFieldVisibility()
        {
            // Ocultar o mostrar sección de datos personales según el crédito
            PersonalDataSection.IsVisible = _creditLimit == 0;
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            try
            {
                var file = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Selecciona una imagen",
                    FileTypes = FilePickerFileType.Images
                });

                if (file != null && IsImage(file.FileName))
                {
                    ImageNameLabel.Text = file.FileName;
                    SavePersistentData(); // Guardar cambios persistentes
                }
                else
                {
                    await DisplayAlert("Error", "Por favor selecciona un archivo de imagen válido (jpg, png, etc.).", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un problema al seleccionar la imagen: {ex.Message}", "OK");
            }
        }

        private async void OnSelectPdfClicked(object sender, EventArgs e)
        {
            try
            {
                var file = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Selecciona un archivo PDF",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.iOS, new[] { "com.adobe.pdf" } },
                            { DevicePlatform.Android, new[] { "application/pdf" } },
                            { DevicePlatform.WinUI, new[] { ".pdf" } }
                        })
                });

                if (file != null && file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    PdfNameLabel.Text = file.FileName;
                    SavePersistentData(); // Guardar cambios persistentes
                }
                else
                {
                    await DisplayAlert("Error", "Por favor selecciona un archivo PDF válido.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un problema al seleccionar el PDF: {ex.Message}", "OK");
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Guardar los datos persistentes antes de regresar
            SavePersistentData();

            // Regresar a la página anterior
            await Navigation.PopAsync();
        }
    }
}

