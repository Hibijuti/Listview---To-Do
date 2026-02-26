using System.Collections.ObjectModel;

namespace ToDoMaui_Listview;

public partial class MainPage : ContentPage
{
    ObservableCollection<ToDoClass> todoList = new();
    int idCounter = 1;
    ToDoClass selectedItem;

    public MainPage()
    {
        InitializeComponent();
        todoLV.ItemsSource = todoList;
    }

    private void AddToDoItem(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(titleEntry.Text))
            return;

        todoList.Add(new ToDoClass
        {
            id = idCounter++,
            title = titleEntry.Text,
            detail = detailsEditor.Text
        });

        ClearFields();
    }

    private void DeleteToDoItem(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id = int.Parse(btn.ClassId);

        var itemToDelete = todoList.FirstOrDefault(x => x.id == id);
        if (itemToDelete != null)
            todoList.Remove(itemToDelete);
    }

    private void TodoLV_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        selectedItem = (ToDoClass)e.SelectedItem;

        titleEntry.Text = selectedItem.title;
        detailsEditor.Text = selectedItem.detail;

        addBtn.IsVisible = false;
        editBtn.IsVisible = true;
        cancelBtn.IsVisible = true;
    }

    private void EditToDoItem(object sender, EventArgs e)
    {
        if (selectedItem == null) return;

        selectedItem.title = titleEntry.Text;
        selectedItem.detail = detailsEditor.Text;

        todoLV.ItemsSource = null;
        todoLV.ItemsSource = todoList;

        ClearFields();
        ResetButtons();
    }

    private void CancelEdit(object sender, EventArgs e)
    {
        ClearFields();
        ResetButtons();
    }

    private void ClearFields()
    {
        titleEntry.Text = "";
        detailsEditor.Text = "";
    }

    private void ResetButtons()
    {
        addBtn.IsVisible = true;
        editBtn.IsVisible = false;
        cancelBtn.IsVisible = false;
        todoLV.SelectedItem = null;
    }
}