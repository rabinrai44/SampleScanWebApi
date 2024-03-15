import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SoundService {

  constructor() { }


  playSelect() {
    const audio = new Audio();
    audio.src = 'assets/sounds/select.mp3';
    audio.load();
    audio.play();
  }

  playSuccess() {
    const audio = new Audio();
    audio.src = 'assets/sounds/success.mp3';
    audio.load();
    audio.play();
  }

  playError() {
    const audio = new Audio();
    audio.src = 'assets/sounds/error.mp3';
    audio.load();
    audio.play();
  }

  playBeep() {
    const audio = new Audio();
    audio.src = 'assets/sounds/beep.mp3';
    audio.load();
    audio.play();
  }

  playBuzz() {
    const audio = new Audio();
    audio.src = 'assets/sounds/buzzer.mp3';
    audio.load();
    audio.play();
  }

  playTransition() {
    const audio = new Audio();
    audio.src = 'assets/sounds/transition.mp3';
    audio.load();
    audio.play();
  }
}
