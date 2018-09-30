using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevelopersNotebook.UserControls
{
  public partial class SvgIcon : UserControl
  {
    private static readonly PathGeometry defaultGeometry =
      (PathGeometry) Application.Current.FindResource("Question");


    public SvgIcon()
    {
      InitializeComponent();
    }

    public PathGeometry DataGeometry
    {
      get => (PathGeometry) GetValue(DataGeometryProperty);
      set => SetValue(DataGeometryProperty, value);
    }


    public static readonly DependencyProperty DataGeometryProperty =
      DependencyProperty.Register("DataGeometry", typeof(PathGeometry),
        typeof(SvgIcon),
        new PropertyMetadata(defaultGeometry));


    public Brush ShapeFill
    {
      get => (Brush) GetValue(ShapeFillProperty);
      set => SetValue(ShapeFillProperty, value);
    }


    public static readonly DependencyProperty ShapeFillProperty =
      DependencyProperty.Register("ShapeFill", typeof(Brush), typeof(SvgIcon),
        new PropertyMetadata(Brushes.DarkGray));


    public int IconHeight
    {
      get => (int) GetValue(IconHeightProperty);
      set => SetValue(IconHeightProperty, value);
    }


    public static readonly DependencyProperty IconHeightProperty =
      DependencyProperty.Register("IconHeight", typeof(int), typeof(SvgIcon),
        new PropertyMetadata(15));


    public int IconWidth
    {
      get => (int) GetValue(IconWidthProperty);
      set => SetValue(IconWidthProperty, value);
    }


    public static readonly DependencyProperty IconWidthProperty =
      DependencyProperty.Register("IconWidth", typeof(int), typeof(SvgIcon),
        new PropertyMetadata(15));
  }
}