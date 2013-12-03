//Authored by Nathan Beattie

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Reflection;
using System.Xml.Serialization;

namespace ImgTest
{
    public partial class GestureTrackerForm : Form
    {
        //video device
        VideoCaptureDevice videoSource;
        int frameCount = 0;
        int ticks = 0;
        string s = "";

        //color box location
        int[] points = new int[4];
        int[] points2 = new int[4];
        int[] middlePoint = new int[]{0,0};
        int[] prevMiddlePoint = new int[2];

        //mouse double-click location
        int[] mouseDblClick = new int[2];

        //tracking colour
        int[] trackColour = new int[3];
        byte[] prevFrame = new byte[0];
        int avgPositionX = 0;
        int avgPositionY = 0;

        //gesture creation sequence
        LinkedList<int> gestureSequenceCreationList = new LinkedList<int>();

        bool ticked = true; //used to alert the rest of the program when timer has ticked
        bool initialized = false; //when true, gestures can be initialized

        //classes
        GestureTree myGestures = new GestureTree();
        ObjTracker objTracker = new ObjTracker();
        Methods methods = new Methods();
        public GestureTrackerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when GestureTrackerForm has loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestureTrackerForm_Load(object sender, EventArgs e)
        {
            //set initial variables
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            timer1.Interval = 50;
            InitializeNodes();
            gestureSequenceCreationList.AddFirst(10); //add an initial value to the sequence creation list. easy way to avoid NullReferenceExceptions.
            mouseDblClick[0] = -1;
            mouseDblClick[1] = -1;

            //Check if atleast one video source is available.
            //source code borrowed from AForge code examples
            if (videosources != null)
            {
                //For example use first video device. You may check if this is your webcam.
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);
                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Search for the highest resolution
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                                highestSolution = videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                            s = highestSolution;
                        }
                    }
                }
                catch { }
                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);
                //Start recording
                videoSource.Start();

                //start the timer
                if (!timer1.Enabled)
                {
                    timer1.Start();
                }

                //set GestureListBox parameters
                GestureListBox.SelectionMode = SelectionMode.One;
                //set remove gesture button to 'disabled'
                GestureCurrentRemoveButton.Enabled = false;
                //empty required labels
                GestureCurrentNameVar.Text = "";
                GestureCurrentMethodVar.Text = "";
                GestureCurrentSequenceVar.Text = "";
                GestureCurrentDescriptionVar.Text = "";
            }
        }

        /// <summary>
        /// This code is executed when a new frame is grabbed from the camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Cast the frame as Bitmap object and don't forget to use ".Clone()" otherwise
            //you'll probably get access violation exceptions
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            //flip the image horizontaly, so that it's a mirror image of user
            img.RotateFlip(RotateFlipType.RotateNoneFlipX);

            img = DetectColour(img);
            //draw a red box around the detected colour
            
            if (DistanceMoved() != 0 && points[2] != 0 && points[3] != 0)
            {
                for (int x = points[0]; x <= points[1]; x++)
                {
                    img.SetPixel(x, points[2], Color.Red);
                    img.SetPixel(x, points[3], Color.Red);
                }
                for (int y = points[2]; y <= points[3]; y++)
                {
                    img.SetPixel(points[0], y, Color.Red);
                    img.SetPixel(points[1], y, Color.Red);
                }
            }
            //timer has gone off, recalculate middle point of colour
            if (ticked)
            {
                prevMiddlePoint[0] = middlePoint[0];
                prevMiddlePoint[1] = middlePoint[1];
                //middlePoint[0] = (points[0] + points[1]) / 2;
                //middlePoint[1] = (points[2] + points[3]) / 2;
                ticked = false;
            }
            //set the imageBox image
            PicBox.Image = img;
            frameCount++;
        }

        /// <summary>
        /// Code executed when Form1 is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestureTrackerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Stop and free the webcam object if application is closing
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }

            //exit the application
            Application.Exit();
        }

        /// <summary>
        /// unlocks bits in bitmap. provides much faster image manipulation
        /// code taken from MSDN site: http://msdn.microsoft.com/en-us/library/5ey6h79d%28v=vs.80%29.aspx
        /// </summary>
        /// <param name="original">The Bitmap passed to the method</param>
        /// <returns>Returns the modified bitmap</returns>
        private Bitmap DetectColour(Bitmap original)
        {
            // Create a new bitmap.
            Bitmap bmp = original;

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Set every fourth value to 255. A 32bpp bitmap will look red.
            //%4 0 = blue, 1 = green, 2 = red, 3 = alpha

            //variables used to determine box dimensions
            int x = -1;
            int y = -1;
            int width = bmp.Width;
            int height = bmp.Height;
            int largestX = 0;
            int smallestX = width - 1;
            int largestY = 0;
            int smallestY = 0;

            int pixelNumber = 0;
            //go through image, search for specified colour
            for (int counter = 0; counter < rgbValues.Length; counter += 3) //24bit image produced by webcam. +=3 because 3 channels: r, g, b.
            {
                //determine the current x/y co-ordinates of the stream
                x++;
                if (x % width == 0)
                {
                    x = 0;
                    y++;
                }

                //convert to YCbCr color space
                //this gives us a color value relatively independant of luminousity
                double yColour = 0.257 * rgbValues[counter + 2] + 0.504 * rgbValues[counter + 1] + 0.098 * rgbValues[counter] + 16;
                double crColour = 0.439 * rgbValues[counter + 2] - 0.368 * rgbValues[counter + 1] - 0.071 * rgbValues[counter] + 128;
                double cbColour = -0.148 * rgbValues[counter + 2] - 0.291 * rgbValues[counter + 1] + 0.439 * rgbValues[counter] + 128;

                if (prevFrame.Length > 1)
                {
                    double yColourPrev = 0.257 * prevFrame[counter + 2] + 0.504 * prevFrame[counter + 1] + 0.098 * prevFrame[counter] + 16;
                    double crColourPrev = 0.439 * prevFrame[counter + 2] - 0.368 * prevFrame[counter + 1] - 0.071 * prevFrame[counter] + 128;
                    double cbColourPrev = -0.148 * prevFrame[counter + 2] - 0.291 * prevFrame[counter + 1] + 0.439 * prevFrame[counter] + 128;
                    //test the colour threshold, and set min/max x/y values accordingly.
                    //currently tests for skin
                    if (crColour > 130 && crColour < 150 && cbColour > 130 && cbColour < 145 && crColourPrev > 130 && crColourPrev < 150 && cbColourPrev > 130 && cbColourPrev < 145)
                    {
                        /**
                        largestY = y;
                        if (smallestY == 0)
                        {
                            smallestY = y;
                        }
                        if (x < smallestX)
                        {
                            smallestX = x;
                        }
                        if (x > largestX)
                        {
                            largestX = x;
                        }
                         * **/
                        rgbValues[counter] = 255;
                        rgbValues[counter + 1] = 255;
                        rgbValues[counter + 2] = 255;
                        pixelNumber++;
                        avgPositionX += x;
                        avgPositionY += y;
                    }
                }
            }
            if (pixelNumber != 0)
            {
                avgPositionX /= pixelNumber;
                avgPositionY /= pixelNumber;
                MessageBox.Show(pixelNumber.ToString());
            }


            //set previous frame to current frame
            prevFrame = rgbValues;
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            if (smallestY != 0 && largestY != 0)
            {
                points[0] = smallestX;
                points[1] = largestX;
                points[2] = smallestY;
                points[3] = largestY;
            }
            return bmp;
        }

        /// <summary>
        /// get the distance moved since the last frame
        /// </summary>
        /// <returns>Returns the distance moved in pixels</returns>
        private double DistanceMoved()
        {
            double distance = 0;
            int x = 0;
            int y = 0;
            //calculate x/y difference
            x = Math.Abs(middlePoint[0] - prevMiddlePoint[0]);
            y = Math.Abs(middlePoint[1] - prevMiddlePoint[1]);

            //use pythagoras to get distance moved
            distance = Math.Sqrt((x * x) + (y * y));

            return distance;
        }

        /// <summary>
        /// get the direction moved since the last frame
        /// </summary>
        /// <returns>returns an integer representing the direction moved</returns>
        private int DirectionMoved()
        {
            //directions:
            //0 - left.  1 - left/up.  2 - up.  3 - right/up.
            //  4 - right.  5 - right/down.  6 - down.  7 - left/down.
            double direction = 0;
            int xDir = 0;
            int yDir = 0;

            //calculate the direction moved in radians using atan2, then convert to degrees.
            xDir = prevMiddlePoint[0] - middlePoint[0];
            yDir = prevMiddlePoint[1] - middlePoint[1];

            direction = Math.Atan2(yDir, xDir);
            direction *= 180 / Math.PI;

            //assign direction integer based on direction moved.
            if (direction <= 157.5)
            {
                if (direction <= 112.5)
                {
                    if (direction <= 67.5)
                    {
                        if (direction <= 22.5)
                        {
                            if (direction <= -22.5)
                            {
                                if (direction <= -67.5)
                                {
                                    if (direction <= -112.5)
                                    {
                                        if (direction <= -157.5)
                                        {
                                            return 4;
                                        }
                                        return 5;
                                    }
                                    return 6;
                                }
                                return 7;
                            }
                            return 0;
                        }
                        return 1;
                    }
                    return 2;
                }
                return 3;
            }
            return 4;
        }

        /// <summary>
        /// Creates the 'initialization' node
        /// </summary>
        private void InitializeNodes()
        {
            Gesture initialize = new Gesture("Initialize", "N/A", "Must be performed before any other gesture");
            initialize.SetSequence(new int[]{4, 0});
            myGestures.AddGesture(initialize);
            GestureListBox.Items.Add("Initialize");
        }

        /// <summary>
        /// This event triggers each time the timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {            
            //let the rest of the program know that the timer has ticked
            ticked = true;
            ticks++;
            /**
            if (ticks % 30 == 0)
            {
                label1.Text = s;
                frameCount = 0;
            }
             * **/

            label1.Text = avgPositionY.ToString();

            //if movement sequence is complete, determine the gesture and invoke its assigned method.
            if (objTracker.TrackPosition(DistanceMoved(), DirectionMoved()))
            {
                int[] sequence = objTracker.ReturnSequence();
                //test that a gesture exists
                if (myGestures.ReturnGesture(sequence) != null) //gesture exists
                {
                    //return gesture, then invoke method it holds if gesture returning have been initialized
                    Gesture g = myGestures.ReturnGesture(sequence);
                    if (initialized && g.GetName() != "Initialize")
                    {
                        MethodInfo theMethod = typeof(Methods).GetMethod(g.GetFunction());
                        theMethod.Invoke(methods, null);
                    }
                    //initialize gesture input if initialize gesture given
                    else if (g.GetName() == "Initialize")
                    {
                        initialized = !initialized;
                    }
                }
            }
            //textbox stuff
            string str = "";
            if (initialized)
            {
                str = "Awaiting Input...";
            }
            int[] seq = objTracker.ReturnSequenceNoErase();
            if (seq.Length != 0)
            {
                DisplayGestureSequence(GestureInfoLabel, seq);
            }
            else
            {
                GestureInfoLabel.Text = str;
            }

            //control mouse
            //Cursor.Position = new Point((int)(Screen.PrimaryScreen.Bounds.Width * ((double)middlePoint[0] / 640)), (int)(Screen.PrimaryScreen.Bounds.Height * ((double)middlePoint[1] / 480)));
        }

        private void SaveGestures()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Gesture));
            List<Gesture> allGestures = myGestures.ReturnAllGestures();
            for (int i = 0; i < allGestures.Count; i++)
            {
                StreamWriter file = new StreamWriter("Gesture-" + allGestures[i].GetName() + ".xml");
                serializer.Serialize(file, allGestures[i]);
                file.Close();
            }
        }

        private void LoadGestures()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Gesture));
            string[] files = System.IO.Directory.GetFiles(".", "Gesture-" + "*.xml", SearchOption.TopDirectoryOnly);

            for(int i = 0; i < files.Length; i++)
            {
                StreamReader file = new StreamReader(files[i]);
                Gesture gesture = (Gesture)serializer.Deserialize(file);
                file.Close();
                if (gesture.GetName() != "Initialize")
                {
                    CreateGesture(gesture.GetName(), gesture.GetFunction(), gesture.GetDescription(), gesture.GetSequence());
                }
            }
        }
        #region GestureCreation
        //
        /// <summary>
        /// creates a gesture based on the info provided by the user
        /// </summary>
        /// <param name="name">the name of the gesture</param>
        /// <param name="method">the method name for the gesture to invoke</param>
        /// <param name="seq">the movement sequence required to activate the gesture</param>
        private void CreateGesture(string name, string method, string description, int[] seq)
        {
            //validate input
            MethodInfo theMethod = typeof(Methods).GetMethod(method);
            bool isValid = true;
            if (theMethod == null) //method doesn't exist
            {
                MessageBox.Show("METHOD DOESN'T EXIST");
                isValid = false;
            }
            else if (name == "") //no gesture name entered
            {
                MessageBox.Show("NO NAME ENTERED");
                isValid = false;
            }
            else if (seq.Length == 0) //no sequence entered
            {
                MessageBox.Show("NO SEQUENCE ENTERED");
                isValid = false;
            }
            else
            {
                //make sure name and sequence don't match pre-existing gestures
                foreach (Gesture g in myGestures.ReturnAllGestures())
                {
                    if (g.GetName() == name) //name already exists
                    {
                        MessageBox.Show("A GESTURE WITH THIS NAME ALREADY EXISTS");
                        isValid = false;
                        break;
                    }
                    else if (g.GetSequence().Length == seq.Length) //test that the sequence isn't already in use
                    {
                        int[] gSeq = g.GetSequence();
                        isValid = false;
                        for (int i = 0; i < gSeq.Length; i++)
                        {
                            if (gSeq[i] != seq[i])
                            {
                                isValid = true;
                                break;
                            }
                        }
                        if (!isValid) //sequence already exists
                        {
                            MessageBox.Show("A GESTURE WITH THIS SEQUENCE ALREADY EXISTS");
                        }
                    }
                }
            }

            //input valid
            if(isValid)
            {
                //create gesture
                Gesture newGesture = new Gesture(name, method, description);
                newGesture.SetSequence(seq);
                myGestures.AddGesture(newGesture);

                //add new gesture to GestureListBox
                GestureListBox.Items.Add(name);

                //reset textboxes
                GestureMethodBox.Text = "";
                GestureNameBox.Text = "";
                GestureDescriptionBox.Text = "";
                //reset gesture sequence
                gestureSequenceCreationList = new LinkedList<int>();
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
                gestureSequenceCreationList.AddFirst(10);
            }
        }

        /// <summary>
        /// calls the creategesture method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestureCreateButton_Click(object sender, EventArgs e)
        {
            gestureSequenceCreationList.RemoveFirst();
            CreateGesture(GestureNameBox.Text, GestureMethodBox.Text, GestureDescriptionBox.Text, gestureSequenceCreationList.ToArray());
        }

        //button clicks for gesture sequence
        private void GestureCreatorButtonUp_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 2)
            {
                gestureSequenceCreationList.AddLast(2);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonUpRight_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 3)
            {
                gestureSequenceCreationList.AddLast(3);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonRight_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 4)
            {
                gestureSequenceCreationList.AddLast(4);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonDownRight_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 5)
            {
                gestureSequenceCreationList.AddLast(5);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonDown_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 6)
            {
                gestureSequenceCreationList.AddLast(6);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonDownLeft_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 7)
            {
                gestureSequenceCreationList.AddLast(7);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonLeft_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 0)
            {
                gestureSequenceCreationList.AddLast(0);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonUpLeft_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 1)
            {
                gestureSequenceCreationList.AddLast(1);
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        private void GestureCreatorButtonBack_Click(object sender, EventArgs e)
        {
            if (gestureSequenceCreationList.Last() != 10)
            {
                gestureSequenceCreationList.RemoveLast();
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }
        #endregion

        /// <summary>
        /// used to display the gesture sequence as letters in labels
        /// </summary>
        /// <param name="l"></param>
        /// <param name="sequence"></param>
        private void DisplayGestureSequence(Label l, int[] sequence)
        {
            l.Text = "";
            foreach (int i in sequence)
            {
                switch (i)
                {
                    case 0:
                        l.Text += "L, ";
                        break;
                    case 1:
                        l.Text += "UL, ";
                        break;
                    case 2:
                        l.Text += "U, ";
                        break;
                    case 3:
                        l.Text += "UR, ";
                        break;
                    case 4:
                        l.Text += "R, ";
                        break;
                    case 5:
                        l.Text += "DR, ";
                        break;
                    case 6:
                        l.Text += "D, ";
                        break;
                    case 7:
                        l.Text += "DL, ";
                        break;
                    default:
                        break;
                }
            }
        }

        #region GestureViewing

        /// <summary>
        /// ListBox method. used to display the appropriate information about the selected gesture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gesture selectedGesture = null;
            //get gesture based on item selected
            if (GestureListBox.SelectedItem != null)
            {
                selectedGesture = myGestures.ReturnGestureByName(GestureListBox.SelectedItem.ToString());
            }
            //display gesture info
            if (selectedGesture != null)
            {
                GestureCurrentNameVar.Text = GestureListBox.SelectedItem.ToString();
                GestureCurrentMethodVar.Text = selectedGesture.GetFunction();
                GestureCurrentDescriptionVar.Text = selectedGesture.GetDescription();
                DisplayGestureSequence(GestureCurrentSequenceVar, selectedGesture.GetSequence());
                if (selectedGesture.GetName() != "Initialize")
                {
                    GestureCurrentRemoveButton.Enabled = true;
                }
                else
                {
                    GestureCurrentRemoveButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Button to remove a gesture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestureCurrentRemoveButton_Click(object sender, EventArgs e)
        {
            Gesture selectedGesture = myGestures.ReturnGestureByName(GestureListBox.SelectedItem.ToString());
            myGestures.RemoveGesture(selectedGesture);
            GestureListBox.Items.Remove(GestureListBox.SelectedItem);
            GestureCurrentRemoveButton.Enabled = false;

            GestureCurrentNameVar.Text = "";
            GestureCurrentMethodVar.Text = "";
            GestureCurrentSequenceVar.Text = "";
            GestureCurrentDescriptionVar.Text = "";
        }
        #endregion

        private void PicBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mouseDblClick[0] = e.X;
            mouseDblClick[1] = e.Y;
        }

        private void GestureSaveBtn_Click(object sender, EventArgs e)
        {
            SaveGestures();
        }

        private void GestureLoadBtn_Click(object sender, EventArgs e)
        {
            LoadGestures();
        }

        
    }
}
