<template>
    <canvas id="myChart" ></canvas>
     <canvas id="timeChart" ></canvas>
     <canvas id="loginChart"></canvas>
     <canvas id="RegChart"></canvas>
     <canvas id="medChart"></canvas>
     <canvas id="foodchart"></canvas>

</template>

<script>
import Chart from 'chart.js'
    export default {
        name: 'analysisDash',
        mounted(){
            this.getAvg(),
            this.getTime(),
            this.getLoginGraph(),
            this.getRegGraph(),
            this.getMed(),
            this.getFood()
        },
        data() {
        return {
            formData :{
            },
            message : [],
            messageView:[],
            messageLogin:[],
            loginDay:[],
            loginTotal:[],
            viewsAvg:[],
            timeAvg:[],
            views:[],
            time:[],
            chart:{},
            timeView:{},
            loginView:{},
            RegView:{},
            RegDay:[],
            RegTotal:[],
            messageReg: [],
            medMessage:[],
            medSearched:[],
            medAmount:[],
            medView:{},
            foodMessage:{},
            foodSearched:[],
            foodAmount:[],
            foodView:{},
        }
    },
    methods:{
         async getAvg(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
            const response= await fetch(process.env.VUE_APP_BACKEND+'Account/getAvgTime',requestOptions)                
                .then(response =>  response.json())
                .then(body=> this.message= body)
            
            for( var i in this.message){
                this.viewsAvg[i]=this.message[i].viewName
                this.timeAvg[i]=this.message[i].seconds
            }
        this.chart= {
        labels: this.viewsAvg,
        datasets : [
          {
            label: 'Views Average Time',
            backgroundColor: 'rgba(0, 0, 255, 0.75)',
            hoverBackgroundColor: 'rgb(0, 0, 200)',
            data: this.timeAvg,
          }
        ]
      };
      var ctx = document.getElementById('myChart');

      var barGraph = new Chart(ctx, {
        type: 'bar',
        data: this.chart,
        options: {
            scales:{
                y:{
                    title: {
                        display: true,
                        text: 'seconds'
                    }
                },
                beginAtZero: true
            } 
        }
  
      });
        },
        async getTime(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
            const response= await fetch(process.env.VUE_APP_BACKEND+'Account/getTotalTime',requestOptions)                
                .then(response =>  response.json())
                .then(body=> this.messageView= body)

            for( var i in this.messageView){
                this.views[i]=this.messageView[i].viewName
                this.time[i]=this.messageView[i].seconds
            }
        this.timeView= {
        labels: this.views,
        datasets : [
          {
            label: 'Views Total Time',
            backgroundColor: 'rgba(0, 0, 255, 0.75)',
            hoverBackgroundColor: 'rgb(0, 0, 200)',
            data: this.time,
              options: {
              scales: {
                  yAxes: [{
                      ticks: {
                          stepSize: 1
                      }
                  }],
              beginAtZero: true,
              }
              
              }
              
            }
          
        ]
      };
      var ctx = document.getElementById('timeChart');

      var graph = new Chart(ctx, {
        type: 'bar',
        data: this.timeView
  
      });
        },
        async getLoginGraph(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
            const response= await fetch(process.env.VUE_APP_BACKEND+'Account/timeTracker?Type=Login&months=3',requestOptions)                
                .then(response =>  response.json())
                .then(body=> this.messageLogin= body)

            for( var i in this.messageLogin){
                this.loginDay[i]=this.messageLogin[i].date
                this.loginTotal[i]=this.messageLogin[i].count
            }
        this.loginView= {
        labels: this.loginDay,
        datasets : [
          {
            label: 'Logins per day',
            backgroundColor: 'rgba(0, 0, 255, 0.75)',
            hoverBackgroundColor: 'rgb(0, 0, 200)',
            data: this.loginTotal,
          }
        ]
      };
      var ctx = document.getElementById('loginChart');

      var graph = new Chart(ctx, {
        type: 'line',
        data: this.loginView,
        options: {
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 1,
                    beginAtZero: true
                }
            }],

        }
        
        }
      });
        },
                async getRegGraph(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
            const response= await fetch(process.env.VUE_APP_BACKEND+'Account/timeTracker?Type=Registration&months=3',requestOptions)                
                .then(response =>  response.json())
                .then(body=> this.messageReg= body)

            for( var i in this.messageReg){
                this.RegDay[i]=this.messageReg[i].date
                this.RegTotal[i]=this.messageReg[i].count
            }
        this.RegView= {
        labels: this.RegDay,
        datasets : [
          {
            label: 'Registrations Per day per day',
            backgroundColor: 'rgba(0, 0, 255, 0.75)',
            hoverBackgroundColor: 'rgb(0, 0, 200)',
            data: this.RegTotal,
          }
        ]
      };
      var ctx = document.getElementById('RegChart');

      var graph = new Chart(ctx, {
        type: 'line',
        data: this.RegView,
        options: {
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 1
                }
            }]
        }
        }
      });
        },
          async getMed(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
            const response= await fetch(process.env.VUE_APP_BACKEND+'Account/getSearchCount/?type=drug',requestOptions)                
                .then(response =>  response.json())
                .then(body=> this.medMessage= body)
            
            for( var i in this.medMessage){
                this.medSearched[i]=this.medMessage[i].itemSearched
                this.medAmount[i]=this.medMessage[i].occurences
            }
        this.medView= {
        labels: this.medSearched,
        datasets : [
          {
            label: 'Most Searched Drugs',
            backgroundColor: 'rgba(0, 0, 255, 0.75)',
            hoverBackgroundColor: 'rgb(0, 0, 200)',
            data: this.medAmount,
          }
        ]
      };
      var ctx = document.getElementById('medChart');

      var barGraph = new Chart(ctx, {
        type: 'bar',
        data: this.medView,
        options: {
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 1,
                    beginAtZero: true
                }
            }]
        }

                }
  
      });
        },
        async getFood(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
            const response= await fetch(process.env.VUE_APP_BACKEND+'Account/getSearchCount/?type=food',requestOptions)                
                .then(response =>  response.json())
                .then(body=> this.foodMessage= body)
            
            for( var i in this.foodMessage){
                this.foodSearched[i]=this.foodMessage[i].itemSearched
                this.foodAmount[i]=this.foodMessage[i].occurences
            }
        this.foodView= {
        labels: this.foodSearched,
        datasets : [
          {
            label: 'Most Searched Foods',
            backgroundColor: 'rgba(0, 0, 255, 0.75)',
            hoverBackgroundColor: 'rgb(0, 0, 200)',
            data: this.foodAmount,
          }
        ]
      };
      var ctx = document.getElementById('foodchart');

      var barGraph = new Chart(ctx, {
        type: 'bar',
        data: this.foodView,
        options: {
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 1,
                    beginAtZero: true
                }
            }]
        }

                }
  
      });
        },
        
    }
    }

</script>

<style scoped>

</style>