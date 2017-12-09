using System.Windows;
using MMFPCommonDataStructures;

namespace MMFPAdminka
{
    /// <summary>
    /// Логика взаимодействия для EditQuestionDialog.xaml
    /// </summary>
    public partial class EditQuestionDialog : Window
    {
        public EditQuestionDialog(Question question)
            : this(new EditQuestionViewModel(question))
        {
        }

        public EditQuestionDialog(EditQuestionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}