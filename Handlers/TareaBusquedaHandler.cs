using Gestion_de_Tareas.Models;
using Gestion_de_Tareas.ViewModels;
using Gestion_de_Tareas.Views;

namespace Gestion_de_Tareas.Handlers
{
    public class TareaBusquedaHandler : SearchHandler
    {
        private readonly TareasViewModel _vm;

        public TareaBusquedaHandler(TareasViewModel vm)
        {
            _vm = vm;

            ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    },
                    Padding = new Thickness(12, 8),
                    ColumnSpacing = 10
                };

                // Nombre de la tarea
                var nombre = new Label
                {
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center
                };
                nombre.SetBinding(Label.TextProperty, "Nombre");

                // Badge estado — color viene directo del binding ColorEstado
                var badgeBorder = new Border
                {
                    StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 20 },
                    Stroke = Colors.Transparent,
                    Padding = new Thickness(10, 4),
                    VerticalOptions = LayoutOptions.Center
                };
                badgeBorder.SetBinding(Border.BackgroundColorProperty, "ColorEstado");

                var estadoLabel = new Label
                {
                    FontSize = 11,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.White,
                    HorizontalTextAlignment = TextAlignment.Center
                };
                estadoLabel.SetBinding(Label.TextProperty, "Estado");

                badgeBorder.Content = estadoLabel;

                grid.Add(nombre, 0, 0);
                grid.Add(badgeBorder, 1, 0);

                return grid;
            });
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            ItemsSource = string.IsNullOrWhiteSpace(newValue)
                ? null
                : _vm.Tareas
                    .Where(t => t.Nombre.Contains(newValue,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            if (item is Tarea tarea)
            {
                Query = string.Empty;
                var detalle = new DetalleTareaPage(_vm, tarea);
                await Shell.Current.Navigation.PushAsync(detalle);
            }
        }
    }
}