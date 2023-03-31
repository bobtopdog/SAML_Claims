<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="WIF_Claims.error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
           <style>
            
            /*basic reset */
            
            *{
                margin: 0;
                padding: 0;
            }
            
            body {background: black;}
            canvas {display:block;}
        .container {
  height:  50pc;
  position: relative;
  border: 0px ;
}

.center {
  margin: 0;
  position: absolute;
  top: 50%;
  left: 50%;
  -ms-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
}

.buttonR {
  background-color: #FF0000;
  border: none;
  color: white;
  padding: 10px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 10px;
  margin: 0px 0px;
}

.buttonB {
  background-color: #0000FF;
  border: none;
  color: white;
  padding: 10px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 10px;
  margin: 0px 0px;
}

.button5 {border-radius: 47%;}
        </style>
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
        <canvas id="c"></canvas>	

  <div class="center">
      
      <asp:Button ID="Button1" runat="server" Text="" CssClass="buttonR button5" Width="130px" Height="40px" ToolTip="Take the red pill and stay in wonderland" OnClick="Button1_Click" />&nbsp;&nbsp;&nbsp;
  <asp:Button ID="Button2" runat="server" Text="" CssClass="buttonB button5"  Width="130px" Height="40px" ToolTip="Take the blue pill and the story ends" OnClick="Button2_Click"/>

   </div>
</div>       

        <script>
        // geting canvas by id c
        var c = document.getElementById("c");
        var ctx = c.getContext("2d");

        //making the canvas full screen
        c.height = window.innerHeight;
        c.width = window.innerWidth;

        //chinese characters - taken from the unicode charset
        var matrix = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789@#$%^&*()*&^%";
        //converting the string into an array of single characters
        matrix = matrix.split("");

        var font_size = 10;
        var columns = c.width/font_size; //number of columns for the rain
        //an array of drops - one per column
        var drops = [];
        //x below is the x coordinate
        //1 = y co-ordinate of the drop(same for every drop initially)
        for(var x = 0; x < columns; x++)
            drops[x] = 1; 

        //drawing the characters
        function draw()
        {
            //Black BG for the canvas
            //translucent BG to show trail
            ctx.fillStyle = "rgba(0, 0, 0, 0.04)";
            ctx.fillRect(0, 0, c.width, c.height);

            ctx.fillStyle = "#0F0"; //green text
            ctx.font = font_size + "px arial";
            //looping over drops
            for(var i = 0; i < drops.length; i++)
            {
                //a random chinese character to print
                var text = matrix[Math.floor(Math.random()*matrix.length)];
                //x = i*font_size, y = value of drops[i]*font_size
                ctx.fillText(text, i*font_size, drops[i]*font_size);

                //sending the drop back to the top randomly after it has crossed the screen
                //adding a randomness to the reset to make the drops scattered on the Y axis
                if(drops[i]*font_size > c.height && Math.random() > 0.975)
                    drops[i] = 0;

                //incrementing Y coordinate
                drops[i]++;
            }
        }

        setInterval(draw, 35);

        
        </script>
    </form>
</body>
</html>
