using System.Windows;
using MMFPCommonDataStructures;

namespace MMFPAdminka
{
    /// <summary>
    /// Логика взаимодействия для EditQuestionSectionDialog.xaml
    /// </summary>
    public partial class EditQuestionSectionDialog : Window
    {
        public EditQuestionSectionDialog(QuestionSection questionSection)
            : this(new EditQuestionSectionViewModel(questionSection))
        {
        }

        public EditQuestionSectionDialog(EditQuestionSectionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}