var test = {
  UMDelete (username){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            
            };
            fetch('https://localhost:7011/UM/DeleteAccount?username=' +username ,requestOptions)
                .then(response => console.log(response))
        },
  UMEnable(username){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            
            };
            fetch('https://localhost:7011/UM/EnableAccount?username=' +username ,requestOptions)
                .then(response => console.log(response))
        },
 UMDisable(username){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            
            };
            fetch('https://localhost:7011/UM/DisableAccount?username=' +username ,requestOptions)
                .then(response => console.log(response))
        },
UMCreate(Username,Password,Email,FirstName,LastName,isAdmin,isEnabled,privOption){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify(
                    {Username : Username,
                     Password : Password,
                     Email: Email,
                     FirstName: FirstName,
                     LastName: LastName,
                     isAdmin: Number(isAdmin),
                     isEnabled: Number(isEnabled),
                     privOption: Number(privOption),
                     }),
            
            };
            fetch('https://localhost:7011/UM/CreateAccount' ,requestOptions)
        },
UMUpdate(Username,Password,Email,FirstName,LastName,isAdmin,isEnabled,privOption){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify(
                    {Username : Username,
                     Password : Password,
                     Email: Email,
                     FirstName: FirstName,
                     LastName: LastName,
                     isAdmin: Number(isAdmin),
                     isEnabled: Number(isEnabled),
                     privOption: Number(privOption),
                     }),
            
            };
            fetch('https://localhost:7011/UM/UpdateAccount' ,requestOptions)
        },
parseLine(line){
        console.log(line)
        var words = line.split(' ');
        if (words[0] == "create")
        {
  
            this.UMCreate( words[1],words[3],words[2],words[4],words[5],parseInt(words[6]),parseInt(words[7]), 1)
        }
        else if (words[0] == "update")
        {
            console.log("update")
            
        }
        else if (words[0] == "enable")
        {
            console.log("enable")
            
        }
        else if (words[0] == "disable")
        {
            console.log("disable")
            
        }
        else if (words[0] == "delete")
        {
            console.log("delete")
            
        }

    }
}

export default test
