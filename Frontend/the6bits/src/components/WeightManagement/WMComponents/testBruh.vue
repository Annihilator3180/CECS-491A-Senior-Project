var View01 = {
  template: `<button id="view1" @click="swapComponent('view-02')" type="submit">NEXT VIEW</button>`,
  props: ['swapComponent']
}
var View02 = {
  template: `<button id="view2" @click="swapComponent('view-01')" type="submit">BACK TO PREVIOUS VIEW</button>`,
  props: ['swapComponent']
}

new Vue({
  el: '#app',
  data() {
    currentComponent: 'view-01'
  },
  components: {
   'view-01': View01,
    'view-02': View02,
  },
  methods: {
    swapComponent: function(component) {
      this.currentComponent = component;
    }
  }
});