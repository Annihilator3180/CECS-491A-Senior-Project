const GetRecord = (pageNumber) =>{
    const requestOptions = {
        method: "GET",
        headers: { "Content-Type": "application/json",
        "Authorization" : `Bearer ${sessionStorage.getItem('token')}`},
    };
    //maybe chang eto local host for now
    return fetch(process.env.VUE_APP_BACKEND+'HealthRecorder/ViewRecord?lastRecordIndex=' + pageNumber ,requestOptions)
        .then(response => response.text())
        .then(value =>  { 
            return JSON.parse(value)})   
}
const ExportRecord = (recordName, categoryName, recordNumber) =>{
    const requestOptions = {
        method: "GET",
        headers: { "Content-Type": "application/json",
        "Authorization" : `Bearer ${sessionStorage.getItem('token')}`},
    };
    return fetch(process.env.VUE_APP_BACKEND+'HealthRecorder/ExportRecord?recordName=' + recordName + "&categoryName=" + categoryName + "&recordNumber=" + recordNumber ,requestOptions)
        .then(response => response.text())
        .then(value =>{
            console.log(JSON.parse(value))
            return JSON.parse(value)
        });
}
const DeleteRecord = (recordName, categoryName) =>{
    const requestOptions = {
        method: "DELETE",
        headers: { "Content-Type": "application/json",
        "Authorization" : `Bearer ${sessionStorage.getItem('token')}`},
        body: JSON.stringify({recordName :recordName, categoryName : categoryName  })
    };
    return fetch(process.env.VUE_APP_BACKEND+'HealthRecorder/DeleteRecord',requestOptions)
        .then(response => response.text())
        .then(value =>  { 
            return JSON.parse(value)})
        }
const SearchRecord = (recordName, categoryName) =>{
    const requestOptions = {
        method: "GET",
        headers: { "Content-Type": "application/json",
        "Authorization" : `Bearer ${sessionStorage.getItem('token')}`},
    };
    return fetch(process.env.VUE_APP_BACKEND + 'HealthRecorder/SearchRecord?recordName=' + recordName + "&categoryName=" + categoryName, requestOptions )
    .then(response => response.text())
        .then(value =>{
            console.log(JSON.parse(value))
            return JSON.parse(value)
        })
}
           
export {GetRecord, ExportRecord, DeleteRecord, SearchRecord}