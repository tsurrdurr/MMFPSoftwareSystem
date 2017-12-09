using System.Windows;
using MMFPCommonDataStructures;

namespace MMFPAdminka
{
    /// <summary>
    /// Логика взаимодействия для EditAnswerDialog.xaml
    /// </summary>
    public partial class EditAnswerDialog : Window
    {
        public EditAnswerDialog(Question question)
            : this(new EditAnswerViewModel(question))
        {
        }

        public EditAnswerDialog(EditAnswerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}