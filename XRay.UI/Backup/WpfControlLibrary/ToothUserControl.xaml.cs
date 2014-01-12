using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace WpfControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        #region Delegates and Events
        public delegate void CalculationFinishedEventHandler(
            Object sender,
            CalculatedInformation information
        );

        public event CalculationFinishedEventHandler OnCalculationFinished;
        #endregion

        #region Constants
        enum ActiveMode { _7Tooth, _8Tooth, _Bone, _None };

        const int PointBetweenRootsNumber = 8;
        const int PointOnTheTopOfTooth = 1;
        const string WarningMessageCaption = "Інформація";
        #endregion

        #region Fields
        bool shouldBeAddedEdge;
        private Shape _selectedPoint;
        private ToothPolygon _polygon = new ToothPolygon();
        public ToothSet _toothSet = new ToothSet();
        #endregion

        #region Auxiliary lines & Dots
        Line SectioningN1;
        Line SectioningN2;
        Line ProblemToothWidth;
        Line NormalToothTangent;
        ToothPoint NearestPointToBoneOnNormalTooth;
        ToothPoint NearestPointOnProblemToothToNormal;
        Ellipse NearestPointToBoneOnNormalToothEllipse;
        Ellipse NearestPointOnProblemToothToNormalEllipse;
        #endregion

        #region Properties
        private string imageFile;
        private object mouseDownSender;
        public string ImageFile {
            get{
                if (string.IsNullOrEmpty(imageFile))
                {
                    return System.AppDomain.CurrentDomain.BaseDirectory + "DSC00319.JPG";
                } else {
                    return imageFile;
                }
            }
            set { 
                imageFile = value;
                //BitmapImage bi3 = new BitmapImage();
                //bi3.BeginInit();
                //bi3.UriSource = new Uri(imageFile, UriKind.Relative);
                //bi3.EndInit();

                //this.MainImage.Source = bi3;
                //cnvGraph.Children.Add(this.MainImage);
                BitmapImage bmpImage = new BitmapImage((new Uri(ImageFile)));
                ImageBrush ib = new ImageBrush(bmpImage);

                ib.AlignmentX = AlignmentX.Center;
                ib.AlignmentY = AlignmentY.Top;
                ib.Stretch = Stretch.Uniform;
                this.cnvGraph.Background = ib;

                //double h = this.Width / bmpImage.Width;
                //this.Height = TopStackPanel.Height + bmpImage.Height * h;

                this.ButtonLoad_Click(this, new RoutedEventArgs());
            }
        }

        public bool ShowAuxiliaryGeometry
        {
            get { 
                return this.btnShowAuxiliaryGeometry.IsChecked.HasValue &&
                    this.btnShowAuxiliaryGeometry.IsChecked == true;
            }
        }

        private ActiveMode currentMode
        {
            get {
                ActiveMode mode = ActiveMode._None;
                if (CB_N7.IsChecked == true)
                    mode = ActiveMode._7Tooth;
                else if (CB_N8.IsChecked == true)
                    mode = ActiveMode._8Tooth;
                else if (CB_Bone.IsChecked == true)
                    mode = ActiveMode._Bone;
                return mode;
            }
        }

        private ActiveMode prevoiusMode = ActiveMode._None;

        public bool AllObjectsAreIndicated
        {
            get {
                return _toothSet.NormalTooth.IsClosed &&
                    _toothSet.ProblemTooth.IsClosed &&
                    _toothSet.BoneEdge.IsClosed;
            }
        }
 
        #endregion

        public UserControl1()
        {
            InitializeComponent();
            this.CB_N7.IsChecked = true;
            //ImageBrush ib =  new ImageBrush();
            
            //this.cnvGraph.Background = new ImageBrush(
            //    new BitmapImage((
            //        new Uri(System.AppDomain.CurrentDomain.BaseDirectory + imageFile)
            //        ))
            //    );
        }

        #region Event handlers
        private void cnvGraph_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mouseDownSender = sender;
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseDownSender == sender)
            {
                mouseDownSender = null;
                AddPoint();
            }
        }

        private void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //CancelFollowingPoints();
        }

        private void ellipse_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse)
            {
                ToothPoint clickedPoint = (ToothPoint)((Ellipse)sender).DataContext;
                if (_polygon.Points.Contains(clickedPoint))
                {
                    CancelFollowingPoints(clickedPoint, (Shape)sender);
                }
            }
        }

        private void CheckBox_Unhecked(object sender, RoutedEventArgs e)
        {
           
            if (sender == CB_N7)
                this.prevoiusMode = ActiveMode._7Tooth;
            else if (sender == CB_N8)
                this.prevoiusMode = ActiveMode._8Tooth;
            else if (sender == CB_Bone)
                this.prevoiusMode = ActiveMode._Bone;

            if (CB_N7.IsChecked == false && CB_N8.IsChecked == false && CB_Bone.IsChecked == false)
            {
                ((CheckBox)sender).IsChecked = true;
                return;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {         
            this.CB_N7.IsChecked = sender == CB_N7;
            this.CB_N8.IsChecked = sender == CB_N8;
            this.CB_Bone.IsChecked = sender == CB_Bone;

            if (sender == CB_N7)
            {
                setLastEdgeForSwitchedPolygon(_polygon);
                //_toothSet.ProblemTooth = this._polygon;
                saveCurrentPolygon(prevoiusMode);

                this._polygon = this._toothSet.NormalTooth;
                //this._polygon.MaxPoligonPointsNumber = ToothSet.MaxNormalToothPointsCount;


                //cnvGraph.Children.Clear();

                DrawAllPolygons(false);
                _selectedPoint = _polygon.LastSetEdge;

            }
            else if (sender == CB_N8)
            {

                setLastEdgeForSwitchedPolygon(_polygon);

                //_toothSet.NormalTooth = this._polygon;            
                saveCurrentPolygon(prevoiusMode);

                this._polygon = this._toothSet.ProblemTooth;
                //this._polygon.MaxPoligonPointsNumber = ToothSet.MaxProblemToothPointsCount;



                DrawAllPolygons(false);
                _selectedPoint = _polygon.LastSetEdge;


            }
            else if (sender == CB_Bone)
            {
                setLastEdgeForSwitchedPolygon(_polygon);
                //_toothSet.NormalTooth = this._polygon;
                saveCurrentPolygon(prevoiusMode);

                this._polygon = this._toothSet.BoneEdge;
                //this._polygon.MaxPoligonPointsNumber = ToothSet.MaxBonePointsCount;

                DrawAllPolygons(false);
                _selectedPoint = _polygon.LastSetEdge;
            }
        }

        public void SaveState()
        {
            ButtonSave_Click(this, new RoutedEventArgs());
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            //ofd.OpenFile();
            //bool? DialogResult = ofd.ShowDialog();
            //if (DialogResult.HasValue && DialogResult.Value)
            //{
            using (Stream writer = File.Open(GetXMLFileName() + ".xml", FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer formatter =
                    new System.Xml.Serialization.XmlSerializer(_toothSet.GetType());
                formatter.Serialize(writer, _toothSet);
            }
            //}
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (!AllObjectsAreIndicated)
            {
                MessageBox.Show("Для початку обрахунку відмітьте контури всіх об'єктів.",
                    WarningMessageCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //ClearAuxiliaryPrimitives();
            //CalculateAuxiliaryGeometry();
            DrawAuxiliaryPrimitives();

            //Point p = Mathematics.FindNearestPoligonPointToGivenPoint(
            //    _toothSet.NormalTooth.GetPolygon(),
            //    _toothSet.BoneEdge.Points[0].GetPoint());
            
            //Ellipse ellipse;

            //if (ShowAuxiliaryGeometry)
            //    ellipse = CreateCircleObject(4, Color.FromRgb(0, 0, 255), p.X, p.Y);

            ////Point bottom_p = Mathematics.FindMostCloseXProjectionPoint(p, _toothSet.ProblemTooth.GetPolygon(),
            ////    _toothSet.IsRightOrientation ? 1 : -1, 1);


            //ToothPoint closestPoint = FindNearestPointOnProblemToothByAngles(p);
            //if (ShowAuxiliaryGeometry)
            //    ellipse = CreateCircleObject(4, Color.FromRgb(0, 0, 255), closestPoint.X, closestPoint.Y);
            
            //Line firstLine = new Line();
            //firstLine.X1 = p.X;
            //firstLine.Y1 = p.Y;
            //firstLine.X2 = closestPoint.X;
            //firstLine.Y2 = closestPoint.Y;
            
            //if (ShowAuxiliaryGeometry)
            //    DrawLine(p, new Point(closestPoint.X, closestPoint.Y), Color.FromRgb(0, 0, 255), false);
            

            ////Find tooth split line
            //Line secondLine = GetSectioningLineN1(firstLine);


            //DrawLine(secondLine, Color.FromRgb(0, 0, 255), true);


            ////Find tooth split 2 line
            //Point center = Mathematics.GetLineCenter(secondLine);
            //ToothPoint tp_betweenRoots = _toothSet.ProblemTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single();
            //Point betweenRoots = tp_betweenRoots.GetPoint();
            //DrawLine(center, betweenRoots, Color.FromRgb(0, 0, 255), true);

            ////Find problem tooth width
            //Line problemToothWidth = FindProblemToothWidthLine();
            //if (ShowAuxiliaryGeometry)
            //    DrawLine(problemToothWidth, Color.FromRgb(255, 0, 0), false);

            ButtonInfo_Click(this, new RoutedEventArgs());
        }

        private Line GetSectioningLineN1(Line firstLine)
        {
            Line secondLine = Mathematics.GetParallelLineThroughThePoint(firstLine, _toothSet.BoneEdge.Points[0].GetPoint());
            List<Point> list = Mathematics.GetIntersectionOf(secondLine, _toothSet.ProblemTooth.GetPolygon());
            secondLine = Mathematics.GetLongestLineFromPoints(list);
            return secondLine;
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            string file = GetXMLFileName() + @".xml";
            ClearAuxiliaryPrimitives();
            if (!File.Exists(file))
            {
                _toothSet = new ToothSet();
                _selectedPoint = null;
                _polygon = new ToothPolygon();
                setMaxPloygonPointsCount(currentMode);
                cnvGraph.Children.Clear();
                this.CB_N7.IsChecked = false;
                this.CB_N7.IsChecked = true;
                return;
            }

            using (Stream reader = File.Open(file, FileMode.OpenOrCreate))
            {
                System.Xml.Serialization.XmlSerializer formatter =
                    new System.Xml.Serialization.XmlSerializer(_toothSet.GetType());

                try
                {
                    _toothSet = (ToothSet)formatter.Deserialize(reader);
                }
                catch {
                    MessageBox.Show("Файл з контуром для даного знімка є пошкодженим і буде перестворений.");
                }

                //shouldBeAddedEdge = true;
                _selectedPoint = null;
                _polygon = new ToothPolygon();
                _polygon.MaxPoligonPointsNumber = _toothSet.ProblemTooth.MaxPoligonPointsNumber;
                cnvGraph.Children.Clear();
                foreach (ToothPoint p in _toothSet.ProblemTooth.Points)
                {
                    AddPoint(p);
                }

                _toothSet.ProblemTooth = _polygon;

                _selectedPoint = null;
                _polygon = new ToothPolygon();
                _polygon.MaxPoligonPointsNumber = _toothSet.NormalTooth.MaxPoligonPointsNumber;
                foreach (ToothPoint p in _toothSet.NormalTooth.Points)
                {
                    AddPoint(p);
                }
                _toothSet.NormalTooth = _polygon;

                _selectedPoint = null;
                _polygon = new ToothPolygon();
                _polygon.MaxPoligonPointsNumber = _toothSet.BoneEdge.MaxPoligonPointsNumber;
                foreach (ToothPoint p in _toothSet.BoneEdge.Points)
                {
                    AddPoint(p);
                }
                _toothSet.BoneEdge = _polygon;

                _polygon = _toothSet.NormalTooth;
                //shouldBeAddedEdge = false;

                //saveActivePlygon(currentMode);
            }
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            CalculatedInformation information = new CalculatedInformation();
            information.AngulationAngle = Math.Round(GetAngulation(), 0);
            information.DepthOfRetensy = GetDepthOfRetensy();

            string InfoText;
            InfoText = "Кут ангуляції: " + Math.Round(GetAngulation(), 0).ToString();
            InfoText += System.Environment.NewLine;

            InfoText += System.Environment.NewLine;

            double len1 = GetProblemToothWidth();
            double len2 = GetTooth7_BoneDistance();

            if (len1 < len2)
            {
                InfoText += "Зуб може бути видаленим без дроблення.";
                information.ToothSectioning = CalculatedInformation.Sectioning.NotNeeded;
                information.DistalBoneCutNeeded = false;
            }
            else if (ToothCanBeRemovedBySingleDivision())
            {
                InfoText += "Зуб може бути видаленим за допомогою одного розбиття.";
                information.ToothSectioning = CalculatedInformation.Sectioning.Single;
                information.DistalBoneCutNeeded = false;
            }
            else if (ToothCanBeRemovedByTwoDivisions(GetFirstDivisionLine()))
            {
                InfoText += "Зуб може бути видаленим за допомогою подвійного дроблення.";
                information.ToothSectioning = CalculatedInformation.Sectioning.Double;
                information.DistalBoneCutNeeded = false;
            }
            else
            {
                InfoText += "Зуб не може бути видаленим без хірургічного втручання.";
                information.DistalBoneCutNeeded = true;
            }

            //Calculate angle between tooth#7 and sectioning line #1
            information.SectioningN1Angle = GetAngleBetweenToothN7AndSectionitnLineN1();
            if (EvaluateTypeAngleOfWorkingTool(SectioningN1) > 0)
            {
                information.SectioningN1AngleTypeText = "зовнішній";
            }
            else if (EvaluateTypeAngleOfWorkingTool(SectioningN1) < 0)
            {
                information.SectioningN1AngleTypeText = "внутрішній";
            }

            if (information.SectioningN1Angle == 0)
            {
                information.SectioningN1AngleTypeText = string.Empty;
            }

            if (OnCalculationFinished != null)
                OnCalculationFinished(this, information);
            //MessageBox.Show(InfoText, "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private double GetAngleBetweenToothN7AndSectionitnLineN1()
        {
            Line l = new Line();
            l.X1 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single().GetPoint().X; 
            l.Y1 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single().GetPoint().Y; 
            l.X2 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointOnTheTopOfTooth).Single().GetPoint().X; 
            l.Y2 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointOnTheTopOfTooth).Single().GetPoint().Y;

            double angle = Mathematics.GetAngleBetweenTwiLines1212(SectioningN1, l);
            angle = Math.Round(angle * (180 / Math.PI), 0); ;
            if (angle > 90 && angle <= 180)
            {
                angle = 180 - angle;
            }
            return angle;
        }

        private void cnvGraph_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ContextMenuTextBox.Text = "X: " + Mouse.GetPosition(cnvGraph).X.ToString() + "; Y:" +
                (Mouse.GetPosition(this).Y - Convert.ToInt32(MainGrid.RowDefinitions[0].Height.Value)).ToString();
        }

        private void btnShowAuxiliaryGeometry_Click(object sender, RoutedEventArgs e)
        {
            if (!AllObjectsAreIndicated)
            {
                return;
            }

            ValidateAuxiliaryPrimitives();
            CalculateAuxiliaryGeometry();
            DrawAuxiliaryPrimitives();
        }
        #endregion

        #region Drawing
        private void AddPoint()
        {
            AddPoint(CreateNewCustomPoint());
        }

        private void AddPoint(ToothPoint point)
        {
            if (_polygon.Points.Count == _polygon.MaxPoligonPointsNumber)
            {
                return;
            }

            //ToothPoint point = CreateNewCustomPoint();
            shouldBeAddedEdge = true;
            DrawPoint(point);
            shouldBeAddedEdge = false;

        }

        private void CancelFollowingPoints(ToothPoint point, Shape activeShape)
        {
            Shape _selectedPointTemp = activeShape;
            //cnvGraph.Children.Clear();


            if (point.OrderNumber > 1)
            {
                //Get previous point
                ToothPoint currentPoint = ((ToothPoint)_selectedPointTemp.DataContext);
                Edge e = (from c in _polygon.Points[currentPoint.OrderNumber - 2].Edges
                          where c.EndPoint.OrderNumber == currentPoint.OrderNumber
                          select c).SingleOrDefault();

                ToothPoint previousPoint = e.StartPoint;

                foreach (UIElement element in cnvGraph.Children)
                {
                    if (element is Ellipse && ((Ellipse)element).DataContext == previousPoint)
                    {
                        _selectedPointTemp = (Ellipse)element;
                        break;
                    }
                }
            }
            else
            {
                _selectedPointTemp = null;
                //shouldBeAddedEdge = false;
            }


            _polygon.CancelFollowingPoints(point.OrderNumber);
            //_polygon.DrawGraph(DrawPoint, DrawLine);
            DrawAllPolygons(false);
            _selectedPoint = _selectedPointTemp;


        }

        private void DrawGraph(ToothPolygon polygon)
        {
            if (polygon != null)
            {
                polygon.DrawGraph(DrawPoint, DrawLine);
            }
        }

        private void DrawAllPolygons()
        {
            DrawAllPolygons(true);
        }

        private void DrawAllPolygons(bool DrawAuxiliaryLines)
        {
            cnvGraph.Children.Clear();
            if (CB_N7.IsChecked == true)
            {
                DrawGraph(_toothSet.ProblemTooth);
                _selectedPoint = null;
                DrawGraph(_toothSet.NormalTooth);
                DrawGraph(_toothSet.BoneEdge);
            }
            else if (CB_N8.IsChecked == true)
            {
                DrawGraph(_toothSet.NormalTooth);
                _selectedPoint = null;
                DrawGraph(_toothSet.ProblemTooth);
                DrawGraph(_toothSet.BoneEdge);
            }
            else if (CB_Bone.IsChecked == true)
            {
                DrawGraph(_toothSet.BoneEdge);
                _selectedPoint = null;
                DrawGraph(_toothSet.ProblemTooth);
                DrawGraph(_toothSet.NormalTooth);
            }

            if (DrawAuxiliaryLines)
            {
                ValidateAuxiliaryPrimitives();
                DrawAuxiliaryPrimitives();
            }
        }

        private ToothPoint CreateNewCustomPoint()
        {
            Point cursorPosition = this.TranslatePoint(Mouse.GetPosition(this), cnvGraph);
            ToothPoint resultPoint = new ToothPoint() { OrderNumber = _polygon.Points.Count + 1, X = (int)cursorPosition.X, Y = (int)cursorPosition.Y };
            return resultPoint;
        }

        private void DrawPoint(ToothPoint point)
        {
            Ellipse ellipse = CreateCircleObject(ToothPoint.RADIUS, Color.FromRgb(255, 0, 0), point.X, point.Y);

            //ellipse.MouseLeftButtonUp += new MouseButtonEventHandler(point_MouseLeftButtonUp);
            //ellipse.ContextMenu = cnvGraph.ContextMenu;
            //ellipse.ContextMenuOpening += new ContextMenuEventHandler(ellipse_ContextMenuOpening);
            ellipse.MouseRightButtonUp += new MouseButtonEventHandler(ellipse_MouseRightButtonUp);

            if (shouldBeAddedEdge)
            {
                AddNewPointToGraph(point);
            }
            //        DeselecteCurrentShape();

            _selectedPoint = ellipse;
            ellipse.DataContext = point;

            TextBlock name = new TextBlock();
            name.Text = string.Format("({0})", point.OrderNumber.ToString());
            name.SetValue(Canvas.TopProperty, (double)(point.Y));
            name.SetValue(Canvas.LeftProperty, (double)(point.X + 10));
            name.SetValue(Canvas.ZIndexProperty, 1);
            name.FontSize = 8;
            SolidColorBrush textBrush = new SolidColorBrush();
            textBrush.Color = Color.FromRgb(225, 0, 0);
            name.Foreground = textBrush;
            cnvGraph.Children.Add(name);
            ValidateAuxiliaryPrimitives();
        }

        private Ellipse CreateCircleObject(int Raduis, Color color, double CenterX, double centerY)
        {
            Ellipse ellipse = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = color;
            ellipse.Fill = brush;
            ellipse.StrokeThickness = 1;
            ellipse.Stroke = Brushes.Black;
            ellipse.Width = Raduis * 2;
            ellipse.Height = Raduis * 2;
            cnvGraph.Children.Add(ellipse);
            ellipse.SetValue(Canvas.TopProperty, (double)(centerY - ToothPoint.RADIUS));
            ellipse.SetValue(Canvas.LeftProperty, (double)(CenterX - ToothPoint.RADIUS));
            ellipse.SetValue(Canvas.ZIndexProperty, 1);
            return ellipse;
        }

        private void AddNewPointToGraph(ToothPoint point)
        {
            if (_selectedPoint != null)
            {
                ToothPoint selectedPoint = _selectedPoint.DataContext as ToothPoint;
                if (selectedPoint != null)
                {
                    _polygon.Points.Add(point);
                    Edge edge = new Edge(selectedPoint, point);
                    selectedPoint.Edges.Add(edge);
                    point.Edges.Add(edge);
                    DrawLine(edge, point.OrderNumber == _polygon.MaxPoligonPointsNumber);

                    if (point.OrderNumber == _polygon.MaxPoligonPointsNumber)
                    {
                        Edge finalEdge = new Edge(point, (ToothPoint)_polygon.Points[0]);
                        point.Edges.Add(finalEdge);
                        ((ToothPoint)_polygon.Points[0]).Edges.Add(finalEdge);
                        DrawLine(finalEdge);
                        shouldBeAddedEdge = false;
                        DrawAllPolygons();
                    }
                }
            }
            else
            {
                _polygon.Points.Add(point);
            }
        }

        private void DrawLine(Point startPoint, Point endPoint, Color color, bool dashed)
        {
            Line line = new Line();
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;
            DrawLine(line, color, dashed);

        }

        private void DrawLine(Line line, Color color)
        {
            DrawLine(line, color, false);
        }

        private void DrawLine(Line line, Color color, bool dashed)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = color;

            line.StrokeThickness = 1;
            line.Stroke = brush;
            line.SetValue(Canvas.ZIndexProperty, 10);

            if (dashed)
            {
                DoubleCollection dashes = new DoubleCollection();
                dashes.Add(3);
                dashes.Add(3);
                line.StrokeDashArray = dashes;
                line.StrokeDashCap = PenLineCap.Round;
            }

            if (!cnvGraph.Children.Contains(line))
                cnvGraph.Children.Add(line);
        }

        private void DrawLine(Edge edge)
        {
            DrawLine(edge, false);
        }

        private void DrawLine(Edge edge, bool graphIsComplete)
        {
            //DeselecteCurrentShape();

            Line line = new Line();
            line.X1 = edge.StartPoint.X;
            line.Y1 = edge.StartPoint.Y;
            line.X2 = edge.EndPoint.X;
            line.Y2 = edge.EndPoint.Y;
            //TBD: line.MouseLeftButtonUp += new MouseButtonEventHandler(edge_MouseLeftButtonUp);
            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = graphIsComplete ? Color.FromRgb(0, 255, 0) : Color.FromRgb(255, 0, 0);
            //_selectedPoint = line;

            line.DataContext = edge;
            line.StrokeThickness = 1;
            line.Stroke = brush;
            //line.Stretch = Stretch.Uniform;

            cnvGraph.Children.Add(line);
            //int distance = edge.Distance;
            //TextBlock name = new TextBlock();
            //name.Text = distance.ToString();
            //name.SetValue(Canvas.TopProperty, (double)((edge.EndPoint.Y + edge.StartPoint.Y) / 2));
            //name.SetValue(Canvas.LeftProperty, (double)((edge.EndPoint.X + edge.StartPoint.X) / 2));
            //name.SetValue(Canvas.ZIndexProperty, 1);
            //name.FontSize = 8;
            //SolidColorBrush textBrush = new SolidColorBrush();
            //textBrush.Color = Color.FromRgb(255, 255, 0);
            //name.Foreground = textBrush;
            //cnvGraph.Children.Add(name);
        }
        #endregion

        #region Auxiliary trash
        private void ValidateAuxiliaryPrimitives()
        {
            if (!_toothSet.IsClosed || !this.ShowAuxiliaryGeometry)
            {
                ClearAuxiliaryPrimitives();
            }
        }

        private void ClearAuxiliaryPrimitives()
        {
            cnvGraph.Children.Remove(SectioningN1);
            cnvGraph.Children.Remove(SectioningN2);
            cnvGraph.Children.Remove(ProblemToothWidth);
            cnvGraph.Children.Remove(NormalToothTangent);
            cnvGraph.Children.Remove(NearestPointToBoneOnNormalToothEllipse);
            cnvGraph.Children.Remove(NearestPointOnProblemToothToNormalEllipse);

            SectioningN1 = null;
            SectioningN2 = null;
            ProblemToothWidth = null;
            NormalToothTangent = null;
            NearestPointToBoneOnNormalTooth = null;
            NearestPointOnProblemToothToNormal = null;
        }

        private void CalculateAuxiliaryGeometry()
        {
            Point p = Mathematics.FindNearestPoligonPointToGivenPoint(
                          _toothSet.NormalTooth.GetPolygon(),
                          _toothSet.BoneEdge.Points[0].GetPoint());
            NearestPointToBoneOnNormalTooth = new ToothPoint();
            NearestPointToBoneOnNormalTooth.X = (int)p.X;
            NearestPointToBoneOnNormalTooth.Y = (int)p.Y;


            NearestPointOnProblemToothToNormal = FindNearestPointOnProblemToothByAngles(p);


            Line line = new Line();
            line.X1 = NearestPointToBoneOnNormalTooth.X;
            line.Y1 = NearestPointToBoneOnNormalTooth.Y;
            line.X2 = NearestPointOnProblemToothToNormal.X;
            line.Y2 = NearestPointOnProblemToothToNormal.Y;
            NormalToothTangent = line;

            //Find tooth split line
            SectioningN1 = GetSectioningLineN1(NormalToothTangent);

            //Find tooth split 2 line
            Point center = Mathematics.GetLineCenter(SectioningN1);
            ToothPoint tp_betweenRoots = _toothSet.ProblemTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single();
            Point betweenRoots = tp_betweenRoots.GetPoint();
            SectioningN2 = new Line();
            SectioningN2.X1 = center.X;
            SectioningN2.Y1 = center.Y;
            SectioningN2.X2 = tp_betweenRoots.X;
            SectioningN2.Y2 = tp_betweenRoots.Y;

            //Find problem tooth width

            ProblemToothWidth = FindProblemToothWidthLine();

        }

        private void DrawAuxiliaryPrimitives()
        {
            if (_toothSet.IsClosed)
            {
                ClearAuxiliaryPrimitives();
                CalculateAuxiliaryGeometry();
                DrawSectioningPrimitives();
                if (this.ShowAuxiliaryGeometry)
                {
                    NearestPointToBoneOnNormalToothEllipse = CreateCircleObject(4, Color.FromRgb(0, 0, 255), NearestPointToBoneOnNormalTooth.X, NearestPointToBoneOnNormalTooth.Y);
                    NearestPointOnProblemToothToNormalEllipse = CreateCircleObject(4, Color.FromRgb(0, 0, 255), NearestPointOnProblemToothToNormal.X, NearestPointOnProblemToothToNormal.Y);

                    DrawLine(NormalToothTangent, Color.FromRgb(0, 0, 255), false);
                    DrawLine(ProblemToothWidth, Color.FromRgb(255, 0, 0), false);
                }
            }
        }

        private void DrawSectioningPrimitives()
        {
            DrawLine(SectioningN1, Color.FromRgb(0, 0, 255), true);
            DrawLine(SectioningN2, Color.FromRgb(0, 0, 255), true);
        }
        #endregion

        private void saveCurrentPolygon(ActiveMode mode)
        {
            if (mode == ActiveMode._8Tooth)
            {
                _toothSet.ProblemTooth = _polygon;
            }
            else if (mode == ActiveMode._7Tooth)
            {
                _toothSet.NormalTooth = _polygon;
            }
            else if (mode == ActiveMode._Bone)
            {
                _toothSet.BoneEdge = _polygon;
            }
        
        }

        private void setLastEdgeForSwitchedPolygon(ToothPolygon polygon)
        {
            if (!polygon.IsClosed)
            {
                polygon.LastSetEdge = _selectedPoint;
            }
        }

        private void saveActivePlygon(ActiveMode mode)
        {
            switch (mode)
            {
                case ActiveMode._7Tooth: _toothSet.NormalTooth = _polygon;
                    break;
                case ActiveMode._8Tooth: _toothSet.ProblemTooth = _polygon;
                    break;
                case ActiveMode._Bone: _toothSet.BoneEdge = _polygon;
                    break;
            }
        }

        private void setMaxPloygonPointsCount(ActiveMode mode)
        {
            switch (mode)
            {
                case ActiveMode._7Tooth:
                    _polygon.MaxPoligonPointsNumber = ToothSet.MaxNormalToothPointsCount;
                    break;
                case ActiveMode._8Tooth:
                    _polygon.MaxPoligonPointsNumber = ToothSet.MaxProblemToothPointsCount;
                    break;
                case ActiveMode._Bone:
                    _polygon.MaxPoligonPointsNumber = ToothSet.MaxBonePointsCount;
                    break;
            }
        }

        private string GetXMLFileName()
        {
            //return System.IO.Path.GetFileNameWithoutExtension(ImageFile);
            return System.IO.Path.GetDirectoryName(ImageFile) + 
                System.IO.Path.DirectorySeparatorChar +
                System.IO.Path.GetFileNameWithoutExtension(ImageFile);
        }    

        private void initializePoligonAfterLoad()
        {
            switch (currentMode)
            {
                case ActiveMode._7Tooth:
                    _polygon = _toothSet.NormalTooth;
                    break;
                case ActiveMode._8Tooth:
                    _polygon = _toothSet.ProblemTooth;
                    break;
                case ActiveMode._Bone:
                    _polygon = _toothSet.BoneEdge;
                    break;

            }
        }

        private ToothPoint FindNearestPointOnProblemToothByAngles(Point PointOnNormalTooth)
        {
            Line l1 = new Line();
            l1.X1 = PointOnNormalTooth.X;
            l1.Y1 = PointOnNormalTooth.Y;
            l1.X2 = _toothSet.BoneEdge.Points[0].X;
            l1.Y2 = _toothSet.BoneEdge.Points[0].Y;

            Line l2 = new Line();
            l2.X1 = PointOnNormalTooth.X;
            l2.Y1 = PointOnNormalTooth.Y;

            Double angle =  Double.MinValue;
            Double angle1;
            ToothPoint closestPoint = _toothSet.ProblemTooth.Points[0];
            foreach (ToothPoint pp in _toothSet.ProblemTooth.Points)
            {
                l2.X2 = pp.X;
                l2.Y2 = pp.Y;

                angle1 = Mathematics.GetAngleBetweenTwiLines1212(l1, l2);
                if (angle1 > angle)
                {
                    closestPoint = pp;
                    angle = angle1;
                }

                Debug.WriteLine(pp.ToString() + " - acos(x) = " + angle1.ToString() + " | " + (angle1 * 180 / Math.PI).ToString());

            }
            return closestPoint;

        }

        #region Logic
        private double GetProblemToothWidth()
        {
            Line line = FindProblemToothWidthLine();
            return Mathematics.GetDistanceBetweenTwoPoints(line);
        }

        private double GetTooth7_BoneDistance()
        {
            Point p = Mathematics.FindNearestPoligonPointToGivenPoint(
                _toothSet.NormalTooth.GetPolygon(),
                _toothSet.BoneEdge.Points[0].GetPoint());

            return Mathematics.GetDistanceBetweenTwoPoints(p,
                _toothSet.BoneEdge.Points[0].GetPoint());
        }

        private double GetAngulation()
        {
            Line line1 = new Line();
            Line line2 = new Line();

            ToothPoint tp_Problem_1 = _toothSet.ProblemTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single();
            ToothPoint tp_Problem_2 = _toothSet.ProblemTooth.Points.Where(x => x.OrderNumber == PointOnTheTopOfTooth).Single();

            ToothPoint tp_Norm_1 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single();
            ToothPoint tp_Norm_2 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointOnTheTopOfTooth).Single();
            
            line1.X1 = tp_Problem_1.GetPoint().X;
            line1.Y1 = tp_Problem_1.GetPoint().Y;
            line1.X2 = tp_Problem_2.GetPoint().X;
            line1.Y2 = tp_Problem_2.GetPoint().Y;

            line2.X1 = tp_Norm_1.GetPoint().X;
            line2.Y1 = tp_Norm_1.GetPoint().Y;
            line2.X2 = tp_Norm_2.GetPoint().X;
            line2.Y2 = tp_Norm_2.GetPoint().Y;

            double atan = Mathematics.GetAngleBetweenTwiLines1221(line1, line2);
            double grads = atan * (180 / Math.PI);
            return Math.Abs(grads);
        }

        private Line FindProblemToothWidthLine()
        {
            Line result = new Line();

            ToothPoint tp_1 = _toothSet.ProblemTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single();
            ToothPoint tp_2 = _toothSet.ProblemTooth.Points.Where(x => x.OrderNumber == PointOnTheTopOfTooth).Single();
            Line MiddleLine = Mathematics.GetLongestLineFromPoints(
                new List<Point> { tp_1.GetPoint(), tp_2.GetPoint() });

            List<Point> list = Mathematics.FindMostDistantPolygonPointsUsingPerpendicularToMiddleLine(MiddleLine,
                _toothSet.ProblemTooth.GetPolygon());

            if (list != null && list.Count == 2)
            {
                result = Mathematics.GetLongestLineFromPoints(list); 
            }
            return result;

        }

        private bool ToothCanBeRemovedBySingleDivision()
        {
            Line div_line = GetFirstDivisionLine();
            double div_line_length = Mathematics.GetDistanceBetweenTwoPoints(div_line);
            double len1;
            double len2;

            List<Point> list = Mathematics.GetIntersectionOf(div_line, _toothSet.ProblemTooth.GetPolygon());
            if (list.Count > 2)
            {
                len1 = Mathematics.GetDistanceBetweenTwoPoints(
                    new Point(list[0].X, list[0].Y),
                    new Point(list[1].X, list[1].Y));

                len2 = Mathematics.GetDistanceBetweenTwoPoints(
                    new Point(list[2].X, list[2].Y),
                    new Point(list[3].X, list[3].Y));

                return len1 < div_line_length && len2 < div_line_length;
            }
            else
            {                
                len2 = GetTooth7_BoneDistance();
                
                return len2 >= div_line_length;
            }
        }

        private bool ToothCanBeRemovedByTwoDivisions(Line FirstDivisioLine)
        {

            Line line = GetFirstDivisionLine();
            List<Point> list = Mathematics.GetIntersectionOf(line, _toothSet.ProblemTooth.GetPolygon());

            Line line_precise = Mathematics.GetLongestLineFromPoints(list);

            //now get line which goes through the senter of division line and point netween roots
            Line center_line = new Line();
            Point center_point = Mathematics.GetLineCenter(line_precise);
            ToothPoint tp_Problem_1 = _toothSet.ProblemTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single();

            center_line.X1 = center_point.X;
            center_line.Y1 = center_point.Y;
            center_line.X2 = tp_Problem_1.X;
            center_line.Y2 = tp_Problem_1.Y;

            Point p11 = new Point(line_precise.X1, line_precise.Y1);
            Point p12 = Mathematics.GetIntersectionOfLineAndPerpendicularFromPoint(center_line, p11);
            
            Point p21 = new Point(line_precise.X2, line_precise.Y2);
            Point p22 = Mathematics.GetIntersectionOfLineAndPerpendicularFromPoint(center_line, p21);

            double distance1 = Mathematics.GetDistanceBetweenTwoPoints(p11, p12);
            double distance2 = Mathematics.GetDistanceBetweenTwoPoints(p21, p22);

            double main_dist = GetTooth7_BoneDistance();
            return distance1 < main_dist && distance2 < main_dist;
        }

        private Line GetFirstDivisionLine()
        {
            Point p = Mathematics.FindNearestPoligonPointToGivenPoint(
                _toothSet.NormalTooth.GetPolygon(),
                _toothSet.BoneEdge.Points[0].GetPoint());


            ToothPoint closestPoint = FindNearestPointOnProblemToothByAngles(p);
            
            Line firstLine = new Line();
            firstLine.X1 = p.X;
            firstLine.Y1 = p.Y;
            firstLine.X2 = closestPoint.X;
            firstLine.Y2 = closestPoint.Y;

            //Find tooth split line
            Line secondLine = Mathematics.GetParallelLineThroughThePoint(firstLine, _toothSet.BoneEdge.Points[0].GetPoint());
            List<Point> list = Mathematics.GetIntersectionOf(secondLine, _toothSet.ProblemTooth.GetPolygon());
            secondLine = Mathematics.GetLongestLineFromPoints(list);

            return secondLine;
        }

        private Utils.CalculatedInformation.Retensy GetDepthOfRetensy()
        {
            Utils.CalculatedInformation.Retensy result = CalculatedInformation.Retensy.ND;

            Point p = Mathematics.FindNearestPoligonPointToGivenPoint(
            _toothSet.NormalTooth.GetPolygon(),
            _toothSet.BoneEdge.Points[0].GetPoint());

            ToothPoint tp = FindNearestPointOnProblemToothByAngles(p);

            ToothPoint tp_1 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == 2).Single();
            ToothPoint tp_2 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == 4).Single();
            ToothPoint tp_3 = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == 5).Single();

            if (tp.Y >= tp_1.Y && tp.Y < tp_2.Y)
            {
                result = CalculatedInformation.Retensy.A;
            }
            else if (tp.Y >= tp_2.Y && tp.Y < tp_3.Y)
            {
                result = CalculatedInformation.Retensy.B;
            }
            else if (tp.Y >= tp_3.Y)
            {
                result = CalculatedInformation.Retensy.C;
            }
            return result;
        }

        /// <summary>
        /// 1  - Outside
        /// -1 - Inside
        /// 0  - Zero degrees
        /// </summary>
        /// <returns></returns>
        private int EvaluateTypeAngleOfWorkingTool(Line separationLine)
        {
            int result = 0;
            Point normalTooth_bottom = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointBetweenRootsNumber).Single().GetPoint();
            Point normalTooth_top = _toothSet.NormalTooth.Points.Where(x => x.OrderNumber == PointOnTheTopOfTooth).Single().GetPoint(); 

            Point separation_bottom = separationLine.X1 < separationLine.X2 ? 
                new Point (separationLine.X1, separationLine.Y1) : new Point (separationLine.X2, separationLine.Y2);
            Point separation_top = separationLine.X1 < separationLine.X2 ? 
                new Point (separationLine.X2, separationLine.Y2) : new Point (separationLine.X1, separationLine.Y1);

            Point point_dist_bottom = Mathematics.GetIntersectionOfLineAndPerpendicularFromPoint(separationLine, normalTooth_bottom);
            Point point_dist_top = Mathematics.GetIntersectionOfLineAndPerpendicularFromPoint(separationLine, normalTooth_top);
            double dist_bottom = Mathematics.GetDistanceBetweenTwoPoints(point_dist_bottom, normalTooth_bottom);
            double dist_top = Mathematics.GetDistanceBetweenTwoPoints(point_dist_top, normalTooth_top);


            if (dist_bottom < dist_top)
            {
                result = 1;
            }
            else if (dist_bottom > dist_top)
            {
                result = -1;
            }

            return result;
        }
        #endregion
    }
}
