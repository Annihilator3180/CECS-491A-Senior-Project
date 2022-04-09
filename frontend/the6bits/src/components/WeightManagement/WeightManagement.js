const ReadGoal = () =>{
            const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
            };
            return fetch('https://localhost:7011/WeightManagement/ReadGoal' ,requestOptions)
                .then(response => response.text())
                .then(value =>  { 
                    return JSON.parse(value)})   
        }

const HasWeightGoal = () => {
    return ReadGoal().then(value => JSON.stringify(value) !== '{}')
}


const GoalRequest = (goalModel, requestType) => {
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({GoalWeight : goalModel.weight, GoalDate : goalModel.goaldate, ExerciseLevel: goalModel.calories   })
            };
            return fetch('https://localhost:7011/WeightManagement/'+requestType ,requestOptions)
                .then(response =>  response.text())
                .then(value =>  { 
                    return value})
        }



export {ReadGoal,GoalRequest,HasWeightGoal}