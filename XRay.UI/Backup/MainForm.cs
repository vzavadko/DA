using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using XRay.UI.Core;
using WpfControlLibrary;
using Utils;

namespace XRay.UI
{
    public partial class MainForm : Form
    {
        #region Fields
        private PatientInfo _currentPatientInfo = null;
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof (PatientInfo));
        #endregion

        #region Properties
        public PatientInfo CurrentPatientInfo
        {
            get { return _currentPatientInfo; }
            set
            {
                _currentPatientInfo = value;
                if (!IsPatientInfoDeserializing)
                    InvokeCurrentPatientInfoChanged(new EventArgs());
            }
        }

        public String FilePath { get; set; }
        public String ImageFilePath { get; set; }

        private bool IsPatientInfoDeserializing { get; set; }
        #endregion

        #region Events
        private event EventHandler CurrentPatientInfoChanged;

        private void InvokeCurrentPatientInfoChanged(EventArgs e)
        {
            EventHandler handler = CurrentPatientInfoChanged;
            if (handler != null) handler(this, e);
        }
        #endregion

        // METHODS
        public MainForm()
        {
            InitializeComponent();

            InitializeSaveFileDialog();
            InitializeOpenFileDialog();
            InitializeAttachXRayImageDialog();

            //button1.Click += AddImageClick;

            OnPatientInfoChanged(null, null);

            CurrentPatientInfoChanged += OnPatientInfoChanged;
            tableLayoutPanel1.ColumnCount = 2;
            this.Width -= tableLayoutPanel3.Width;

            this.userControl11.OnCalculationFinished += this.OnCalculationFinished;
        }

        private void OnCalculationFinished(object sender, CalculatedInformation info)
        {
            label_1.Text = info.DepthOfRetensyText;
            label_2.Text = info.AngulationType;
            label_3.Text = info.AngulationAngleText;
            label_4.Text = info.AngulationLevelText;
            label_5.Text = info.SрreadFormingNeeded ? "потребується" : "не потребується";
            label_6.Text = info.ToothSectioningText_1;
            label_6_1.Text = info.ToothSectioningText_2;
            label_7.Text = info.CheeckBoneCutNeeded ? "потребується" : "не потребується";
            label_8.Text = info.DistalBoneCutNeeded ? "потребується" : "не потребується";
            label_9.Text = info.SurgicalInjuryLevel;
            label_10.Text = info.EstimatedDuration;
            label_11.Text = info.OperationComplexityLevel;
            label_6_2.Text = info.SectioningN1AngleText +
                (info.SectioningN1AngleTypeText != string.Empty ? (", " + info.SectioningN1AngleTypeText) : "");
            

            ShowInformationPanel();
        }

        private void OnPatientInfoChanged(object sender, EventArgs e)
        {
            if (CurrentPatientInfo== null)
            {
                firstNameTextBox.Text = String.Empty;
                firstNameTextBox.Enabled = false;

                lastNameTextBox.Text = String.Empty;
                lastNameTextBox.Enabled = false;

                birthDateTimePicker.Value = birthDateTimePicker.MinDate;
                birthDateTimePicker.Enabled = false;

                notesTextBox.Text = String.Empty;
                notesTextBox.Enabled = false;

                textDentalNumber.Text = String.Empty;
                textDentalNumber.Enabled = false;

                button1.Enabled = false;

                return;
            }
            textDentalNumber.Text = CurrentPatientInfo.ToothNumber;
            textDentalNumber.Enabled = true;

            firstNameTextBox.Text = CurrentPatientInfo.FirstName;
            firstNameTextBox.Enabled = true;

            lastNameTextBox.Text = CurrentPatientInfo.LastName;
            lastNameTextBox.Enabled = true;

            birthDateTimePicker.Value = CurrentPatientInfo.DateOfBirth;
            birthDateTimePicker.Enabled = true;

            notesTextBox.Text = CurrentPatientInfo.Notes;
            notesTextBox.Enabled = true;

            //xrayImageBox.Image = CurrentPatientInfo.XRayImages.Count > 0
            //                        ? CurrentPatientInfo.XRayImages.First().ProcessedImage
            //                        : null;

            if (CurrentPatientInfo.XRayImages.Any())
            {
                button1.Text = "Змінити RTG знімок";
            }

            button1.Text = "Додати RTG  знімок";
            button1.Enabled = true;
        }

        private void InitializeSaveFileDialog()
        {
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.DefaultExt = ".xrpi";
            saveFileDialog.Title = "Зберегти картку як ...";
            saveFileDialog.Filter = "Картка пацієнта (*.xrpi)|*.xrpi";
            saveFileDialog.RestoreDirectory = true;
        }

        private void InitializeAttachXRayImageDialog()
        {
            attachXRayImageDialog.AddExtension = true;
            attachXRayImageDialog.CheckPathExists = true;
            attachXRayImageDialog.Title = "Додати знімок ...";
            attachXRayImageDialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            attachXRayImageDialog.RestoreDirectory = true;
        }

        private void InitializeOpenFileDialog()
        {
            openFileDialog.Title = "Відкрити картку пацієнта ...";
            openFileDialog.Filter = "Картка пацієнта (*.xrpi)|*.xrpi";
        }

        private void CreatePatientInfo(object sender, EventArgs e)
        {
            CurrentPatientInfo = new PatientInfo
                                     {
                                         FirstName = String.Empty,
                                         LastName = String.Empty,
                                         ToothNumber = String.Empty,                                         
                                         DateOfBirth = birthDateTimePicker.MinDate,
                                         XRayImages = new List<XRayImage>()
                                     };

            this.userControl11 = new WpfControlLibrary.UserControl1();
            this.elementHost.Child = this.userControl11;
            this.userControl11.OnCalculationFinished += this.OnCalculationFinished;

            this.elementHost_bottom.Visible = false;

            lastNameTextBox.Focus();
            HideInformationPanel();
        }

        private void SavePatientInfo(object sender, EventArgs e)
        {
            SavePatientInfo(false);
        }

        private void SavePatientInfo(bool saveAsMode)
        {
            if (CurrentPatientInfo == null)
            {
                return;
            }

            if (((UserControl1)this.elementHost.Child) != null)
            {
                ((UserControl1)this.elementHost.Child).SaveState();
            }

            UpdatePatientInfo();
            //Copy image to the PatientInfo file location
            if (!String.IsNullOrEmpty(ImageFilePath) &&
                Path.GetDirectoryName(FilePath) != Path.GetDirectoryName(ImageFilePath))
            {

                File.Copy(ImageFilePath, Path.GetDirectoryName(FilePath) + Path.DirectorySeparatorChar + Path.GetFileName(ImageFilePath), true);
                string pathToXML = Path.GetDirectoryName(ImageFilePath) + Path.DirectorySeparatorChar +
                    Path.GetFileNameWithoutExtension(ImageFilePath) + ".xml";
                string newPathToXML = Path.GetDirectoryName(FilePath) + Path.DirectorySeparatorChar +
                    Path.GetFileNameWithoutExtension(ImageFilePath) + ".xml";
                if (File.Exists(pathToXML))
                {
                    File.Copy(pathToXML, newPathToXML, true);
                }
            }
            //Set file image name to CurrentPatientInfo
            CurrentPatientInfo.ImageFileName = Path.GetFileName(ImageFilePath);

            if (!String.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                saveFileDialog.FileName = FilePath;
                //using (StreamWriter sw = new StreamWriter(FilePath))
                //{
                //    Serializer.Serialize(sw, CurrentPatientInfo);
                //}
            }
            else
            {
                saveFileDialog.FileName = String.Empty;
            }

            if (saveAsMode || String.IsNullOrEmpty(FilePath))
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        Serializer.Serialize(sw, CurrentPatientInfo);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(FilePath))
                {
                    Serializer.Serialize(sw, CurrentPatientInfo);
                }
            }
        }

        private void OpenPatientInfo(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream sr = null;

                var filePath = openFileDialog.FileName;

                try
                {
                    //sr = new StreamReader(filePath);
                    sr = File.Open(filePath, FileMode.Open);

                    IsPatientInfoDeserializing = true;
                    CurrentPatientInfo = Serializer.Deserialize(sr) as PatientInfo;
                    IsPatientInfoDeserializing = false;
                    InvokeCurrentPatientInfoChanged(new EventArgs());
                    ShowImage(Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + CurrentPatientInfo.ImageFileName);
                }
                catch(Exception exception)
                {
                    MessageBox.Show(String.Format("Помилка програми.\nДеталі : {0}", exception.Message));
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();

                        FilePath = filePath;
                    }
                    IsPatientInfoDeserializing = false;
                }
            }
        }

        private void ClosePatientInfo(object sender, EventArgs e)
        {
            //CurrentPatientInfo = null;
            Application.Exit();
        }

        private void FirstNameChanged(object sender, EventArgs e)
        {
            //UpdatePatientInfo();
        }

        private void LastNameChanged(object sender, EventArgs e)
        {
            //UpdatePatientInfo();
        }

        private void DateOfBirthChanged(object sender, EventArgs e)
        {
            //UpdatePatientInfo();
        }

        private void NotesChanged(object sender, EventArgs e)
        {
            //UpdatePatientInfo();
        }

        private void UpdatePatientInfo()
        {
            if (CurrentPatientInfo == null)
            {
                return;
            }

            CurrentPatientInfo.FirstName = firstNameTextBox.Text;
            CurrentPatientInfo.LastName = lastNameTextBox.Text;
            CurrentPatientInfo.DateOfBirth = birthDateTimePicker.Value;
            CurrentPatientInfo.ToothNumber = textDentalNumber.Text;
            CurrentPatientInfo.Notes = notesTextBox.Text;
        }

        private void AddImageClick(object sender, EventArgs e)
        {
            if (attachXRayImageDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = attachXRayImageDialog.FileName;
                ShowImage(filePath);
            }
        }

        private void ShowImage(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                return;
            }

            Image xrayImage = null;
            try
            {
                xrayImage = Image.FromFile(filePath);

            }
            catch (Exception exception)
            {
                MessageBox.Show(String.Format("Помилка програми.\nДеталі : {0}", exception.Message));
            }
            ImageFilePath = filePath;


            ((UserControl1)this.elementHost.Child).ImageFile = filePath;
            this.elementHost_bottom.Visible = true;

            HideInformationPanel();
        }

        private void SaveAs(object sender, EventArgs e)
        {
            SavePatientInfo(true);
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    StreamWriter sw = null;

            //    try
            //    {
            //        FilePath = saveFileDialog.FileName;

            //        sw = new StreamWriter(FilePath);

            //        Serializer.Serialize(sw, CurrentPatientInfo);
            //    }
            //    finally
            //    {
            //        if (sw != null)
            //        {
            //            sw.Close();
            //        }
            //    }
            //}
        }

        private void xrayImageBox_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //String filePath=  (String)xrayImageBox.Tag ;
            //if (!String.IsNullOrEmpty(filePath))
            //{
            //    if (CurrentPatientInfo.XRayImages.Any())
            //    {
            //        CurrentPatientInfo.XRayImages[0] = new XRayImage("знимок", filePath);
            //        return;
            //    }

            //    CurrentPatientInfo.XRayImages.Add(new XRayImage("знимок", filePath));
            //    xrayImageBox.Tag = "";
            InvokeCurrentPatientInfoChanged(new EventArgs());
            processImageButton.Enabled = false;

            ShowInformationPanel();
                
                
            //}
        }

        private void ShowInformationPanel()
        {
            if (!tableLayoutPanel3.Visible)
            {
                tableLayoutPanel3.Visible = true;
                tableLayoutPanel1.ColumnCount = 3;
                this.Width += 286;
            }
        }

        private void HideInformationPanel()
        {
            if (tableLayoutPanel3.Visible)
            {
                tableLayoutPanel3.Visible = false;
                tableLayoutPanel1.ColumnCount = 2;
                this.Width -= 286;
            }
        }

        private void інструкціяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Instructions.txt");
        }

    }
}
