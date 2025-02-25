
//let totalHours = parseInt(document.getElementById("totalHours").innerText);
//    let submitBtnNew = document.getElementById("submitBtn");
//    let globalSave=0;
//    updateTotalHours();
//            if(globalSave>0){
//        submitBtnNew.disabled = true;
//    alert(globalSave);
//            }

//    function checkDuplicateSubjects() {
//            debugger;
//    let subjectInputs = document.querySelectorAll('.subject-input');
//    let subjectSet = new Set();

//        subjectInputs.forEach(input => {
//        let value = input.value.trim().toUpperCase();
//    if (value !== "" && subjectSet.has(value)) {
//        alert("Duplicate subject names are not allowed!");
//    input.value = "";
//            } else {
//        subjectSet.add(value);
//            }
//        });
//    }

//    function updateTotalHours() {
//        let allocatedHours = 0;
//    let hourInputs = document.querySelectorAll('.hour-input');

//        hourInputs.forEach(input => {
//        let value = parseInt(input.value) || 0;
//    allocatedHours += value;
//        });

//    document.getElementById("allocatedHours").innerText = allocatedHours;

//    if(totalHours==allocatedHours)
//    {
//        submitBtnNew.disabled = false;
    
//        }
//                   //  alert( allocatedHours +"  " +allocatedHours);

//    }

//    function validateTotalHours() {
//        updateTotalHours();
//    allocatedHours = parseInt(document.getElementById("allocatedHours").innerText);

//    if (allocatedHours !== totalHours) {
//        alert("Total allocated hours must be equal to " + totalHours);
//    globalSave = allocatedHours;
//    return false;
//        }

//    return true;
//    }

