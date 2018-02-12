﻿namespace Acerola.Domain.ValueObjects
{
    public class PIN
    {
        public string Text { get; private set; }

        public PIN(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new PINShouldNotBeEmptyException("The 'PIN' field is required");

            this.Text = text;
        }

        public override string ToString()
        {
            return Text.ToString();
        }
    }
}
