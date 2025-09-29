namespace FirstMauiApp.Pages;

public partial class GridPage : ContentPage
{
    public GridPage()
    {
        InitializeComponent();

        // ---
        // Grid
        Grid grid = new Grid
        {
            ColumnSpacing = 20,
            RowSpacing = 20,

            // набор строк
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            },

            // набор столбцов
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
        };
        PopulateGrid(grid);
        Content = grid;
        // ---
    }


    private void PopulateGrid(Grid grid)
    {
        // добавление элементов по определенным позициям
        // column: 0 - позиция слева, row: 0 - позиция сверху
        // columnSpan: 2 - объединить с ячейкой вправо, rowSpan: 2 - объединить с ячейкой вниз
        // extensions - Microsoft.Maui.Controls.GridExtensions Class

        // первый столбец

        // grid.Add(new BoxView { Color = Color.FromRgb(250, 253, 255) }, column: 0, row: 0);
        grid.AddWithSpan(new BoxView { Color = Color.FromRgb(250, 253, 255) }, row: 0, column: 0, rowSpan: 1, columnSpan: 1);

        // grid.Add(new BoxView { Color = Color.FromRgb(196, 232, 255) }, column: 0, row: 1);
        grid.AddWithSpan(new BoxView { Color = Color.FromRgb(196, 232, 255) }, row: 1, column: 0, rowSpan: 1, columnSpan: 1);

        // grid.Add(new BoxView { Color = Color.FromRgb(133, 207, 255) }, column: 0, row: 2);
        grid.AddWithSpan(new BoxView { Color = Color.FromRgb(133, 207, 255) }, row: 2, column: 0, rowSpan: 1, columnSpan: 1);

        // второй столбец

        // объединенная ячейка - объединяем в столбце 3 ячейки вниз
        // 1
        // grid.Add(new BoxView { Color = Color.FromRgb(87, 189, 255) }, column: 1, row: 0);
        // 2
        // grid.AddWithSpan(new BoxView { Color = Color.FromRgb(87, 189, 255) }, row: 0, column: 1, rowSpan: 3, columnSpan: 1);
        // 3
        var boxView = new BoxView { Color = Color.FromRgb(87, 189, 255) };
        grid.Add(boxView, column: 1, row: 0);
        // Grid.SetColumnSpan(boxView, 1);
        Grid.SetRowSpan(boxView, 3);

        // grid.Add(new BoxView { Color = Color.FromRgb(43, 172, 255) }, column: 1, row: 1);
        // grid.AddWithSpan(new BoxView { Color = Color.FromRgb(43, 172, 255) }, row: 1, column: 1, rowSpan: 1, columnSpan: 1);

        // grid.Add(new BoxView { Color = Color.FromRgb(23, 164, 255) }, column: 1, row: 2);
        // grid.AddWithSpan(new BoxView { Color = Color.FromRgb(23, 164, 255) }, row: 2, column: 1, rowSpan: 1, columnSpan: 1);

        // третий столбец

        // grid.Add(new BoxView { Color = Color.FromRgb(0, 121, 199) }, column: 2, row: 0);
        grid.AddWithSpan(new BoxView { Color = Color.FromRgb(0, 121, 199) }, row: 0, column: 2, rowSpan: 1, columnSpan: 1);

        // grid.Add(new BoxView { Color = Color.FromRgb(0, 76, 199) }, column: 2, row: 1);
        grid.AddWithSpan(new BoxView { Color = Color.FromRgb(0, 76, 199) }, row: 1, column: 2, rowSpan: 1, columnSpan: 1);

        // grid.Add(new BoxView { Color = Color.FromRgb(0, 76, 199) }, column: 2, row: 2);
        grid.AddWithSpan(new BoxView { Color = Color.FromRgb(0, 76, 199) }, row: 2, column: 2, rowSpan: 1, columnSpan: 1);
    }
}
