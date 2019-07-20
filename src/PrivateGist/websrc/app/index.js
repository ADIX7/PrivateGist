import Vue from 'vue';
import AppComponent from './components/AppComponent/AppComponent.vue';
import signalRHandler from './SignalRHandlers';
import { Subject } from 'rxjs';
const signalR = require("@aspnet/signalr");

import "bootstrap";

//SignalR init
var signalRConnection = new signalR.HubConnectionBuilder().withUrl("/apiHub").build();
let signalRApi = signalRHandler.initializeSignalRHandlers(signalRConnection);

//Responsive UI
let vueData = {
  desktopMode: true
}

var vm = new Vue({
  el: '#app',
  render: h => h(AppComponent),
  data() {
    return {
      externalData: vueData
    };
  },
  provide: function () {
    return {
      signalRApi: signalRApi,
      externalData: this.externalData,
      screenModeChanged: this.$options.screenModeChanged,
      signalRStateChanged: this.$options.signalRStateChanged
    }
  },
  screenModeChanged: null,
  signalRStateChanged: null,
  beforeCreate: function () {
    this.$options.screenModeChanged = new Subject();
    this.$options.signalRStateChanged = new Subject();
  }
});

//SignalR handlers 
signalRConnection.onclose(error => vm.$options.signalRStateChanged.error(error));
signalRConnection.start()
  .then(() => vm.$options.signalRStateChanged.next("connected"))
  .catch(error => vm.$options.signalRStateChanged.error(error));


function screenWidthHandler(data) {
  if (data.matches) {
    vueData.desktopMode = false;
  }
  else {
    vueData.desktopMode = true;
  }

  vm.$options.screenModeChanged.next({ desktopMode: vueData.desktopMode });
}

var screenWidthChecker = window.matchMedia("(max-width: 1000px)");
screenWidthChecker.addListener(screenWidthHandler);
screenWidthHandler(screenWidthChecker);