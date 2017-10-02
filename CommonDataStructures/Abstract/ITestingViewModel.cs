namespace MMFPCommonDataStructures
{
    public interface ITestingViewModel
    {
        QuestionSet questionSet { get; set; }
        void GenerateResult();
        void GeneratePDF();
    }
}
