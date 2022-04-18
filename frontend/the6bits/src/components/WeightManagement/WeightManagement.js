const ReadGoal = () =>{
            const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
            };
            return fetch(process.env.VUE_APP_BACKEND+'WeightManagement/ReadGoal' ,requestOptions)
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
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
                body: JSON.stringify({GoalWeight : goalModel.weight, GoalDate : goalModel.goaldate, ExerciseLevel: goalModel.calories   })
            };
            return fetch(process.env.VUE_APP_BACKEND+'WeightManagement/'+requestType ,requestOptions)
                .then(response =>  response.text())
                .then(value =>  { 
                    return value})
        }



const GetAllFoodLogs = () =>{
            const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
            };
            return fetch(process.env.VUE_APP_BACKEND+'WeightManagement/GetFoodLogs' ,requestOptions)
                .then(response => response.text())
                .then(value =>  { 
                    return JSON.parse(value)})   
        }

const JsonLogToCSVString =(logArr)=>{
    let csvContent = "data:text/csv;charset=utf-8,foodname,calories,description,fooddate\n"
        + logArr.map(FoodItem => FoodItem.foodName +','+ FoodItem.calories.toString() +','+ FoodItem.description +','+  FoodItem.foodLogDate.toString()+'\n').join('');
    return csvContent
}

export {ReadGoal,GoalRequest,HasWeightGoal,GetAllFoodLogs,JsonLogToCSVString}