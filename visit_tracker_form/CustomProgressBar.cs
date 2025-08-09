using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class CustomProgressBar : UserControl
{
    private int valor = 0;
    private int maximo = 100;
    private Color corBarra = Color.Green;
    private Color corFundo = Color.LightGray;

    [Category("Custom")]
    public int Valor
    {
        get => valor;
        set
        {
            if (value < 0) value = 0;
            if (value > maximo) value = maximo;
            valor = value;
            Invalidate(); // redesenha
        }
    }

    [Category("Custom")]
    public int Maximo
    {
        get => maximo;
        set
        {
            if (value <= 0) value = 1;
            maximo = value;
            if (valor > maximo) valor = maximo;
            Invalidate();
        }
    }

    [Category("Custom")]
    public Color CorBarra
    {
        get => corBarra;
        set { corBarra = value; Invalidate(); }
    }

    [Category("Custom")]
    public Color CorFundo
    {
        get => corFundo;
        set { corFundo = value; Invalidate(); }
    }

    public CustomProgressBar()
    {
        this.DoubleBuffered = true;
        this.Size = new Size(200, 30);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;

        // Fundo
        using (SolidBrush brushFundo = new SolidBrush(corFundo))
        {
            g.FillRectangle(brushFundo, 0, 0, Width, Height);
        }

        // Barra
        float porcentagem = (float)valor / maximo;
        int larguraBarra = (int)(Width * porcentagem);

        using (SolidBrush brushBarra = new SolidBrush(corBarra))
        {
            g.FillRectangle(brushBarra, 0, 0, larguraBarra, Height);
        }

        // Texto
        string texto = $"{(int)(porcentagem * 100)}%";
        SizeF tamanhoTexto = g.MeasureString(texto, Font);
        g.DrawString(texto, Font, Brushes.Black,
            (Width - tamanhoTexto.Width) / 2,
            (Height - tamanhoTexto.Height) / 2);
    }
}
