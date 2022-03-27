var canvas,
    canvas1,
    canvas2,
    ctx,
    con,
    cin,
    source,
    context,
    analyser,
    fbc_array,
    bar_count,
    bar_count2,
    bar_pos,
    bar_width,
    bar_width1,
    bar_height,
    bar_height1;

var audio = new Audio();

audio.src = "C:/Users/19139/Desktop/MUSIC/mp3/DangelicoNew.mp3";
audio.controls = true;
audio.loop = false;
audio.autoplay = false;

window.addEventListener(
    "load",
    function() {
        canvas = document.getElementById("canvas");
        canvas1 = document.getElementById("canvasLeft");
        canvas2 = document.getElementById("canvasRight");

        ctx = canvas.getContext("2d");
        con = canvas1.getContext("2d");
        cin = canvas2.getContext("2d");

        document.getElementById("audio").appendChild(audio);
        context = new AudioContext();
        analyser = context.createAnalyser();
        source = context.createMediaElementSource(audio);
        source.connect(analyser);
        analyser.connect(context.destination);


        FrameLooper();
    },
    false
);

function FrameLooper() {
    window.RequestAnimationFrame =
        window.requestAnimationFrame(FrameLooper) ||
        window.msRequestAnimationFrame(FrameLooper) ||
        window.mozRequestAnimationFrame(FrameLooper) ||
        window.webkitRequestAnimationFrame(FrameLooper);

    fbc_array = new Uint8Array(analyser.frequencyBinCount);
    analyser.getByteFrequencyData(fbc_array);


    bar_count = window.innerWidth / 2;
    bar_count2 = window.innerHeight / 2;

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    con.clearRect(0,0,canvas1.width,canvas1.height);
    cin.clearRect(0,0,canvas2.width,canvas2.height);
    /*extra steps here to clear rect*/

    /*rainbow gradient*/
    var my_gradient=ctx.createLinearGradient(0, 0,270, 0);
    my_gradient.addColorStop(0, "indigo");
    my_gradient.addColorStop(0.1, "violet");
    my_gradient.addColorStop(0.2, "hotpink");
    my_gradient.addColorStop(0.3, "red");
    my_gradient.addColorStop(0.4, "orange");
    my_gradient.addColorStop(0.5, "yellow");
    my_gradient.addColorStop(0.6, "lime");
    my_gradient.addColorStop(0.7, "green");
    my_gradient.addColorStop(0.8, "aqua");
    my_gradient.addColorStop(0.9, "blue");
    my_gradient.addColorStop(1, "DarkBlue");

    ctx.fillStyle = '#ffffff';
    con.fillStyle = '#ffffff';
    cin.fillStyle = '#ffffff';

    for (var i = 0; i < bar_count; i++) {
        bar_pos = i * 4;
        bar_pos1 = (bar_count2-i)*12;
        bar_width = 2;
        bar_width1 = 8;
        bar_height = -(fbc_array[i] / 2);

        ctx.fillRect(bar_pos, canvas.height, bar_width, bar_height);
        con.fillRect(0,bar_pos1,-bar_height,bar_width1);
        cin.fillRect(canvas2.width,bar_pos1,bar_height,bar_width1);
    }

}
