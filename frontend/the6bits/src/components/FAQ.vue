<template>
  <div class="FAQ">

    <h1>FAQ</h1>
    <H2>Search Food</H2>
        <text>
        Q: "Why cant i save food after searching?" A: You must input a date for what day you ate the food.<br/>
        Q: "How does searching food work?" A: We use Edamam API to handle accurate food searches.<br/>

        </text>
    <H2>Weight Management</H2>
        <text>
        Q: "How is my data calculated?" A:  it is calculated with the weight goald inputted and the Food that is logged.<br/>
        </text>
    <H2>Drug Search</H2>
        <text>
        Q: "What drugs can I search?" A: all drugs approved by fda are searchable<br/>
        Q: "How many drugs can i favorite?" A: you can favorite a maximum of 100<br/>
        Q: "If I unfavorite a drug will it delete the favorited information?" A: yes<br/>
        </text>
    <H2>Health Recorder</H2>
        <text>
        Q: "What kind of files can I save?" A: you can save jpeg or pdf files , up to 16 mb in size<br/>
        Q: "How many files can I save?" A:  you can save up to 1000 total and 10 per day<br/>
        Q: " What can I name my record?" A: you can name it anything as long as it’s between 2 and 100 characters<br/>
        </text>
    <H2>Account Recovery</H2>
        <text>
        Q: "How do i recover my account if i lost my password or if my account is disabled?" A:make sure you're logged out and navigate to the account recovery tab to recover your account<br/>
        Q: "Why can't I recover my account anymore?" A:  we have a limit of 5 account recoveries per month. At the end of the month, this will reset and you can recover your account once again<br/>
        </text>
    <H2>Reminders</H2>
        <text>
        Q: "How many reminders can I create? " A:Limit of 100 Reminders. <br/>
        Q: "Will reminders notify me through email in case I forget?" A: Yes if you opt into email reminders notification. <br/>
        Q: "What happens if I pass the reminders limit?" A: No reminders will be created until you delete some. <br/>
        Q: "What can I add in reminders? " A: You can add a name, description, time and date, as well as if it's supposed to repeat or not. <br/>

        </text>
  </div>
</template>

<script>
export default {
  name: 'FAQ',
  created(){
    window.setInterval(() => { this.timer+=1}, 1000)
  },
  beforeUnmount(){
        this.TimerTime(),
        this.timer=0
    },

  data () {
    return {
      TimerTime(){
      const requestOptions = {
          method: "post",
          headers: { "Content-Type": "application/json",}
      };
      fetch(process.env.VUE_APP_BACKEND+'Account/ViewTime?time='+this.timer+'&view=FAQ',requestOptions)
      },
      msg: 'Welcome to Your Vue.js App',
      timer : 0
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>