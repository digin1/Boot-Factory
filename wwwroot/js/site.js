// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var video = document.getElementById("myvideo");
var playbutton = document.getElementById("playbutton");
var pb = document.getElementById("playbtn");
var sb = document.getElementById("stopbtn");
var slider = document.getElementById("moveSlider");
var goToBttn = document.getElementById("goTo");
var vidSetTime = document.getElementById('videoTime');




function playVideo() {
    if (video.paused) {
        video.play();
        playbutton.innerHTML = "<i class='fa fa-pause'></i>";
    } else {
        video.pause();
        playbutton.innerHTML = "<i class='fa fa-play'></i>";
    }
}

function audio() {
    if (video.muted == false) {
        video.muted = true;
        audiobutton.innerHTML = "<i class='fa fa-volume-off'></i>";
    } else {
        video.muted = false;
        audiobutton.innerHTML = "<i class='fa fa-volume-up'></i>";
    }
}
function play() {
    video.play();
}

function stop() {
    video.pause();
    video.currentTime = "0"
    playbutton.innerHTML = "<i class='fa fa-play'></i>";
}

function dragSlider() {
    video.currentTime = video.duration * (slider.value / 100);
}

function moveSlider() {
    slider.value = video.currentTime / video.duration * 100;
}

function sliderPause() {
    video.pause();
}

function sliderPlay() {
    video.play();
    playbutton.innerHTML = "<i class='fa fa-pause'></i>";
}


sb.addEventListener("click", stop);
slider.addEventListener("change", dragSlider);
video.addEventListener("timeupdate", moveSlider);
slider.addEventListener("inout", sliderPause);
slider.addEventListener("change", sliderPlay);



function SetVolume(val) {
    var player = document.getElementById('myvideo');
    player.volume = val / 100;
}