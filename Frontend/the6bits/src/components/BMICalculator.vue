<template>
    <div class="form">
        <div>
            <H1>BMI Calculator</H1>

            <label for="lbs">Enter Height in cm: </label>
            <input type="double" id="height" v-model="formData.height" />
            <br />
            <label for="lbs">Enter Weight in kg </label>
            <input type="double" id="weight" v-model="formData.weight" />
        </div>
        <button @click="CalculateBMI">Calculate</button>
        {{message}}

        <div>
            <b>BMI Categories</b>
            <p>
                Underweight = 18.5 or less
                <br />
                Normal weight=18.5 to 24.9
                <br />
                Overweight=25 to 29.9
                <br />
                Obesity=BMI of 30 or greater
            </p>
        </div>
    </div>


</template>

<script>
    export default {
        name: 'CalculateBMI',
        data() {
            return {
                res: {},
                formData: {
                    height: 0.0,
                    weight: 0.0,
                },
                message: '',
            }
        },
        methods: {
            CalculateBMI() {
                const requestOptions = {
                    method: "POST",
                    credentials: 'include',
                    headers: { "Authorization": `${sessionStorage.getItem('token')}` }

                };
                    const response =fetch(process.env.VUE_APP_BACKEND + `BMICalculator/Calculate?height=${this.formData.height}&weight=${this.formData.weight}`, requestOptions)
                        .then(response => response.text())
                        .then(body => this.message = body)

            }
        }
    }



</script>

<style scoped>
    label {
        /* Other styling... */
        text-align: right;
        clear: both;
        float: left;
        margin-right: 15px;
    }
</style>
