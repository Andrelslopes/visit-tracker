using System;
using System.Drawing;
using System.Windows.Forms;

public class ProgressBarCustomized : ProgressBar
{
    public ProgressBarCustomized()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Definindo a cor da barra com base no valor
        Color corBarra;

        if (this.Value <= 40)
            corBarra = Color.Red;
        else if (this.Value <= 80)
            corBarra = Color.Orange;
        else
            corBarra = Color.Green;

        // Desenhando a barra de progresso com a cor selecionada
        using (SolidBrush brush = new SolidBrush(corBarra))
        {
            e.Graphics.FillRectangle(brush, 0, 0, (int)(this.Width * ((float)this.Value / this.Maximum)), this.Height);
        }
    }
}