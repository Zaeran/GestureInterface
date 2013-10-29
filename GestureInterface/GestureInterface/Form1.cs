using System;
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

namespace ImgTest
{
    public partial class Form1 : Form
    {
        //video device
        VideoCaptureDevice videoSource;

        //color box location
        int[] points = new int[4];
        int[] middlePoint = new int[2];
        int[] prevMiddlePoint = new int[2];

        //gesture creation sequence
        LinkedList<int> gestureSequenceCreationList = new LinkedList<int>();

        bool ticked = true; //used to alert the rest of the program when timer has ticked
        bool initialized = false; //when true, gestures can be initialized

        //classes
        GestureTree myGestures = new GestureTree();
        ObjTracker objTracker = new ObjTracker();
        Methods methods = new Methods();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            timer1.Interval = 100;
            InitializeNodes();
            gestureSequenceCreationList.AddFirst(10);
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
                GestureCreatorSequenceVar.Text = "";
            }
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Cast the frame as Bitmap object and don't forget to use ".Clone()" otherwise
            //you'll probably get access violation exceptions
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
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
            if (ticked)
            {
                prevMiddlePoint[0] = middlePoint[0];
                prevMiddlePoint[1] = middlePoint[1];
                middlePoint[0] = (points[0] + points[1]) / 2;
                middlePoint[1] = (points[2] + points[3]) / 2;
                ticked = false;
            }
            img.SetPixel(middlePoint[0], middlePoint[1], Color.Green);
            PicBox.Image = LockUnlockBitsExample(img);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Stop and free the webcam object if application is closing
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long startTime = Environment.TickCount;

            GestureInfoLabel.Text = "Elapsed: " + (Environment.TickCount - startTime).ToString() + "ms";
        }

        //unlocks bits in bitmap. provides much faster image manipulation
        //code taken from MSDN site: http://msdn.microsoft.com/en-us/library/5ey6h79d%28v=vs.80%29.aspx
        private Bitmap LockUnlockBitsExample(Bitmap original)
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
                //test the colour threshold, and set min/max x/y values accordingly.
                //currently tests for blue
                if (rgbValues[counter] / 2.5 > rgbValues[counter + 1] && rgbValues[counter] / 2.5 > rgbValues[counter + 2] && rgbValues[counter] > 120)
                {
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
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            points[0] = smallestX;
            points[1] = largestX;
            points[2] = smallestY;
            points[3] = largestY;
            return bmp;
        }

        //get the distance moved since the last frame
        private double DistanceMoved()
        {
            double distance = 0;
            int x = 0;
            int y = 0;
            x = Math.Abs(middlePoint[0] - prevMiddlePoint[0]);
            y = Math.Abs(middlePoint[1] - prevMiddlePoint[1]);

            distance = Math.Sqrt((x * x) + (y * y));

            return distance;
        }
        //get the direction moved since the last frame
        private int DirectionMoved()
        {
            //directions - 4: right. 0 - left. 2 - up. 6 - down
            //ie. 0 is left, increment by 1 every 45 degrees CCW
            double direction = 0;
            int xDir = 0;
            int yDir = 0;

            xDir = prevMiddlePoint[0] - middlePoint[0];
            yDir = prevMiddlePoint[1] - middlePoint[1];

            direction = Math.Atan2(yDir, xDir);
            direction *= 180 / Math.PI;

            if (direction < 157.5)
            {
                if (direction < 112.5)
                {
                    if (direction < 67.5)
                    {
                        if (direction < 22.5)
                        {
                            if (direction < -22.5)
                            {
                                if (direction < -67.5)
                                {
                                    if (direction < -112.5)
                                    {
                                        if (direction < -157.5)
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

        //Create the 'initialization' node
        private void InitializeNodes()
        {
            Gesture initialize = new Gesture("Initialize", "Must be performed before any other gesture");
            initialize.SetSequence(new int[]{4, 0});
            myGestures.AddGesture(initialize);
            GestureListBox.Items.Add("Initialize");
        }

        //timer event
        private void timer1_Tick(object sender, EventArgs e)
        {
            ticked = true;
            //if gesture is complete, determine the correct course of action
            if (objTracker.TrackPosition(DistanceMoved(), DirectionMoved()))
            {
                int[] sequence = objTracker.ReturnSequence();
                //test that a gesture exists
                if (myGestures.ReturnGesture(sequence) != null) //exists
                {
                    //return gesture, then invoke method it holds
                    Gesture g = myGestures.ReturnGesture(sequence);
                    if (initialized)
                    {
                        initialized = false;
                        MethodInfo theMethod = typeof(Methods).GetMethod(g.GetFunction());
                        theMethod.Invoke(methods, null);
                    }
                    else if (g.GetName() == "Initialize")
                    {
                        initialized = true;
                    }
                }
                else if(sequence.Length != 0) //doesn't exist
                {
                    initialized = false;
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
        }

        #region GestureCreation
        //creates a gesture based on the info provided by the user
        private void CreateGesture(string name, string method, int[] seq)
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
                    if (g.GetName() == name)
                    {
                        MessageBox.Show("A GESTURE WITH THIS NAME ALREADY EXISTS");
                        isValid = false;
                        break;
                    }
                    else if (g.GetSequence().Length == seq.Length)
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
                        if (!isValid)
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
                Gesture newGesture = new Gesture(name, method);
                newGesture.SetSequence(seq);
                myGestures.AddGesture(newGesture);

                //add new gesture to GestureListBox
                GestureListBox.Items.Add(name);

                //reset textboxes
                GestureMethodBox.Text = "";
                GestureNameBox.Text = "";
                //reset gesture sequence
                gestureSequenceCreationList = new LinkedList<int>();
                DisplayGestureSequence(GestureCreatorSequenceVar, gestureSequenceCreationList.ToArray());
            }
        }

        
        //calls the creategesture method
        private void GestureCreateButton_Click(object sender, EventArgs e)
        {
            gestureSequenceCreationList.RemoveFirst();
            CreateGesture(GestureNameBox.Text, GestureMethodBox.Text, gestureSequenceCreationList.ToArray());
            gestureSequenceCreationList.AddFirst(10);
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

        //used to display the gesture sequence as letters in labels
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

        //ListBox method. used to display the appropriate information about the selected gesture
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
                DisplayGestureSequence(GestureCurrentSequenceVar, selectedGesture.GetSequence());
                if (selectedGesture.GetName() != "Initialize")
                {
                    GestureCurrentRemoveButton.Enabled = true;
                }
            }
        }

        private void GestureCurrentRemoveButton_Click(object sender, EventArgs e)
        {
            Gesture selectedGesture = myGestures.ReturnGestureByName(GestureListBox.SelectedItem.ToString());
            myGestures.RemoveGesture(selectedGesture);
            GestureListBox.Items.Remove(GestureListBox.SelectedItem);
            GestureCurrentRemoveButton.Enabled = false;
        }
        #endregion

        
    }
}
