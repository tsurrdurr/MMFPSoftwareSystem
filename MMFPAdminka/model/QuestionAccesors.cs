namespace MMFPAdminka.model
{
    public class QuestionAccesors
    {
        public static IQuestionAccessor GetAccessor(QuestionAccessorType type)
        {
            IQuestionAccessor accessor = null;
            switch (type)
            {
                case QuestionAccessorType.STUB: accessor = new StubQuestionAccessor(); break;
            }
            return accessor;
        }
    }

    public enum QuestionAccessorType
    {
        STUB
    }
}