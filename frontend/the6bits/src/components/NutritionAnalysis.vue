<template>
    <div class="form">
        <form id="usrform">
            <div>
                <b>Enter the Recipe Here:</b>

            </div>
            <textarea name="ingr" form="usrform" v-model="formData.ingr" rows="5" cols="20">
             Enter the recipe here</textarea>
            <button @click="NutritionAnalysis" type="button">Analyse Recipe</button>
            <div id="NutritionAnalysis"></div>
        </form>
    </div>
</template>

<script>
    var allData = [];
    var skip = 0;
    var take = 5;

    export default {
        name: "NutritionAnalysis",
        data() {
            return {
                formData: {
                    ingr: "",
                },
                msg: "",
            };
        },
        methods: {
            onLoadMore() {
                console.log("load more clicked");
                if (allData.length === 0 || skip > allData.length) {
                    return;
                }
                let data1 = "";
                for (let i = skip; i < skip + take; i++) {
                    var values = allData[i];
                    data1 +=
                        // clickable link removed  <p> Recipe Name: ${values.label}</p>
                        `<div class = "DietRecommendation">
                    <p> ${values.label.link(values.url)}</p>
                    <img src =${values.image}
                    <p> Ingredients: </p>
                    <p> ${values.ingredientLines}</p>
                    <p> Calories: </p>
                    <p> ${values.calories}</p>
                    <p> For more recipe details please click on the link. </p>
                    </div>`;
                }
                skip += 5;
                document.getElementById("DietRecommendation").innerHTML += data1;
            },

            NutritionAnalysis() {
                var ingredientsArray = this.formData.ingr.split("\n");
                const requestOptions = {
                    method: "POST",
                    credentials: "include",
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${sessionStorage.getItem('token')}`
                    },
                    body: JSON.stringify({ ingr: ingredientsArray }),

                };
                const response = fetch(
                    "https://localhost:7011/NutritionAnalysis/Create",
                    requestOptions
                )
                    .then((response) => response.json())
                    .then((data) => {
                        alert("Nutritional Facts Generated!");

                        if (typeof data.totalNutrients.ENERC_KCAL != "undefined") {
                            var totalCal = Math.round(data.totalNutrients.ENERC_KCAL.quantity);
                        } else {
                            totalCal = "0";
                        }

                        if (typeof data.totalNutrients.FAT != "undefined") {
                            var FAT =
                                Math.round(data.totalNutrients.FAT.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.FAT.unit;
                        } else {
                            FAT = "-";
                        }
                        if (typeof data.totalDaily.FAT != "undefined") {
                            var totalDailyFAT =
                                Math.round(data.totalDaily.FAT.quantity) +
                                " " +
                                data.totalDaily.FAT.unit;
                        } else {
                            totalDailyFAT = "-";
                        }

                        if (typeof data.totalNutrients.FASAT != "undefined") {
                            var FASAT =
                                Math.round(data.totalNutrients.FASAT.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.FASAT.unit;
                        } else {
                            FASAT = "-";
                        }
                        if (typeof data.totalDaily.FASAT != "undefined") {
                            var totalDailyFASAT =
                                Math.round(data.totalDaily.FASAT.quantity) +
                                " " +
                                data.totalDaily.FASAT.unit;
                        } else {
                            totalDailyFASAT = "-";
                        }

                        if (typeof data.totalNutrients.FATRN != "undefined") {
                            var FATRN =
                                Math.round(data.totalNutrients.FATRN.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.FATRN.unit;
                        } else {
                            FATRN = "-";
                        }

                        if (typeof data.totalNutrients.CHOLE != "undefined") {
                            var CHOLE =
                                Math.round(data.totalNutrients.CHOLE.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.CHOLE.unit;
                        } else {
                            CHOLE = "-";
                        }
                        if (typeof data.totalDaily.CHOLE != "undefined") {
                            var totalDailyCHOLE =
                                Math.round(data.totalDaily.CHOLE.quantity) +
                                " " +
                                data.totalDaily.CHOLE.unit;
                        } else {
                            totalDailyCHOLE = "-";
                        }

                        if (typeof data.totalNutrients.NA != "undefined") {
                            var NA =
                                Math.round(data.totalNutrients.NA.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.NA.unit;
                        } else {
                            NA = "-";
                        }
                        if (typeof data.totalDaily.NA != "undefined") {
                            var totalDailyNA =
                                Math.round(data.totalDaily.NA.quantity) +
                                " " +
                                data.totalDaily.NA.unit;
                        } else {
                            totalDailyNA = "-";
                        }

                        if (typeof data.totalNutrients.CHOCDF != "undefined") {
                            var CHOCDF =
                                Math.round(data.totalNutrients.CHOCDF.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.CHOCDF.unit;
                        } else {
                            CHOCDF = "-";
                        }
                        if (typeof data.totalDaily.CHOCDF != "undefined") {
                            var totalDailyCHOCDF =
                                Math.round(data.totalDaily.CHOCDF.quantity) +
                                " " +
                                data.totalDaily.CHOCDF.unit;
                        } else {
                            totalDailyCHOCDF = "-";
                        }

                        if (typeof data.totalNutrients.FIBTG != "undefined") {
                            var FIBTG =
                                Math.round(data.totalNutrients.FIBTG.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.FIBTG.unit;
                        } else {
                            FIBTG = "-";
                        }
                        if (typeof data.totalDaily.FIBTG != "undefined") {
                            var totalDailyFIBTG =
                                Math.round(data.totalDaily.FIBTG.quantity) +
                                " " +
                                data.totalDaily.FIBTG.unit;
                        } else {
                            totalDailyFIBTG = "-";
                        }

                        if (typeof data.totalNutrients.SUGAR != "undefined") {
                            var SUGAR =
                                Math.round(data.totalNutrients.SUGAR.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.SUGAR.unit;
                        } else {
                            SUGAR = "-";
                        }

                        if (typeof data.totalNutrients.SUGARadded != "undefined") {
                            var SUGARadded =
                                Math.round(data.totalNutrients.SUGARadded.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.SUGARadded.unit;
                        } else {
                            SUGARadded = "-";
                        }

                        if (typeof data.totalNutrients.PROCNT != "undefined") {
                            var PROCNT =
                                Math.round(data.totalNutrients.PROCNT.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.PROCNT.unit;
                        } else {
                            PROCNT = "-";
                        }
                        if (typeof data.totalDaily.PROCNT != "undefined") {
                            var totalDailyPROCNT =
                                Math.round(data.totalDaily.PROCNT.quantity) +
                                " " +
                                data.totalDaily.PROCNT.unit;
                        } else {
                            totalDailyPROCNT = "-";
                        }

                        if (typeof data.totalNutrients.VITD != "undefined") {
                            var VITD =
                                Math.round(data.totalNutrients.VITD.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.VITD.unit;
                        } else {
                            VITD = "-";
                        }
                        if (typeof data.totalDaily.VITD != "undefined") {
                            var totalDailyVITD =
                                Math.round(data.totalDaily.VITD.quantity) +
                                " " +
                                data.totalDaily.VITD.unit;
                        } else {
                            totalDailyVITD = "-";
                        }

                        if (typeof data.totalNutrients.CA != "undefined") {
                            var CA =
                                Math.round(data.totalNutrients.CA.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.CA.unit;
                        } else {
                            CA = "-";
                        }
                        if (typeof data.totalDaily.CA != "undefined") {
                            var totalDailyCA =
                                Math.round(data.totalDaily.CA.quantity) +
                                " " +
                                data.totalDaily.CA.unit;
                        } else {
                            totalDailyCA = "-";
                        }

                        if (typeof data.totalNutrients.FE != "undefined") {
                            var FE =
                                Math.round(data.totalNutrients.FE.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.FE.unit;
                        } else {
                            FE = "-";
                        }
                        if (typeof data.totalDaily.FE != "undefined") {
                            var totalDailyFE =
                                Math.round(data.totalDaily.FE.quantity) +
                                " " +
                                data.totalDaily.FE.unit;
                        } else {
                            totalDailyFE = "-";
                        }

                        if (typeof data.totalNutrients.K != "undefined") {
                            var K =
                                Math.round(data.totalNutrients.K.quantity * 10) / 10 +
                                " " +
                                data.totalNutrients.K.unit;
                        } else {
                            K = "-";
                        }
                        if (typeof data.totalDaily.K != "undefined") {
                            var totalDailyK =
                                Math.round(data.totalDaily.K.quantity) +
                                " " +
                                data.totalDaily.K.unit;
                        } else {
                            totalDailyK = "-";
                        }
                        this.msg =
                            '<h1>Nutrition Facts</h1>' +
                            "</div>" +
                            '<table class="table">' +
                            "<thead>" +
                            "<tr>" +
                            '<th colspan="3" class="c">Amount Per Serving</th>' +
                            "</tr>" +
                            "</thead>" +
                            "<tbody>" +
                            "<tr>" +
                            '<th colspan="2"><b>Calories</b></th>' +
                            '<td class="nob">' + totalCal +
                            "</td>" +
                            "</tr>" +
                            '<td colspan="3"><b>% Daily Value*</b></td>' +
                            "</tr>" +
                            "<tr>" +
                            '<th colspan="2"><b>Total Fat</b> ' + FAT +
                            "</th>" +
                            "<td><b>" + totalDailyFAT +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Saturated Fat " +  FASAT +
                            "</th>" +
                            "<td><b>" + totalDailyFASAT +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Trans Fat " + FATRN +
                            "</th>" +
                            "<td></td>" +
                            "</tr>" +
                            "<tr>" +
                            '<th colspan="2"><b>Cholesterol</b> ' + CHOLE +
                            "</th>" +
                            "<td><b>" + totalDailyCHOLE +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            '<th colspan="2"><b>Sodium</b> ' + NA +
                            "</th>" +
                            "<td><b>" + totalDailyNA +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            '<th colspan="2"><b>Total Carbohydrate</b> ' + CHOCDF +
                            "</th>" +
                            "<td><b>" + totalDailyCHOCDF +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Dietary Fiber " + FIBTG +
                            "</th>" +
                            "<td><b>" + totalDailyFIBTG +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Total Sugars " + SUGAR +
                            "</th>" +
                            "<td></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Includes " +  SUGARadded +
                            " Added Sugars</th>" +
                            "<td></td>" +
                            "</tr>" +
                            '<th colspan="2"><b>Protein</b> ' + PROCNT +
                            "</th>" +
                            "<td><b>" + totalDailyPROCNT +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Vitamin D " + VITD +
                            "</th>" +
                            "<td><b>" + totalDailyVITD +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Calcium " + CA +
                            "</th>" +
                            "<td><b>" + totalDailyCA +
                            "</b></td>" +
                            "</tr>" +
                            "<tr>" +
                            "<th>Iron " + FE +
                            "</th>" +
                            "<td><b>" + totalDailyFE +
                            "</b></td>" +
                            "</tr>" +
                            '<tr class="thin-end">' +
                            "<th>Potassium " + K +
                            "</th>" +
                            "<td><b>" + totalDailyK +
                            "</b></td>" +
                            "</tr>" +
                            '<p class="small-info" id="small-nutrition-info">*Percent Daily Values are based on a 2000 calorie diet</p>' +
                            "</section>"
                            ;


                    });
                document.getElementById("NutritionAnalysis").innerHTML = this.msg;
            },

        },
    };
</script>
<style scoped>
    .form {
        width: 300px;
        margin: 0 auto;
    }
</style>