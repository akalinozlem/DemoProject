var _configurations = [];
var _buildingTypes = {};

$(document).ready(function () {
    $("#addBtn").click(function () {
        if (jQuery.isEmptyObject(_buildingTypes)) {
            $('#errorText').text("All building types are saved.");
            $('#errorDiv').show();
        } else {
            $('#saveModal').modal('show');
            $('#cmbBuildingType').removeAttr('disabled');
            FillBuildingTypes();
        }
    });
    $("#saveBtn").click(function () {
        $('#saveModal').modal('hide');
    });
    $("#closeBtn").click(function () {
        ResetInput();
        $('#saveModal').modal('hide');
    });
    $("#closeAlertErrorBtn").click(function () {
        location.reload();
    });
});

function LoadConfigurations() {
    //Fill the _buildingTypes 
    FillArray();

    ResetTable();  
    $.get("../Home/GetConfigurations", function (confs) {
        var imgId;
        _configurations = confs;
        $.map(confs, function (conf) {
            var tempStr = "<tr>";
            tempStr += "<td><div class='d-flex px-2 py-1'><div><img id='img" + conf.buildingType +"' src='' class='avatar avatar-sm me-3 border-radius-lg' alt=''></div><div class='d-flex px-2 py-1'><div class='d-flex flex-column justify-content-center'><h6 class='mb-0 text-sm'>" + conf.buildingType + "</h6></div></div></td>";
            tempStr += "<td class='align-middle text-center'><p class='text-sm font-weight-bold mb-0'>" + conf.buildingCost + " coins</p></td>";
            tempStr += "<td class='align-middle text-center'><p class='text-sm font-weight-bold mb-0'>" + conf.constructionTime + " seconds</p></td>";
            tempStr += "<td><button id='editBtn' name='editBtn' class='btn btn-default btn-sm' onclick='Edit(\"" + conf.id + "\")'><i class='fa fa-edit' style='font-size:12px;color:grey'></i></button><button class='btn btn-default btn-sm' onclick='Delete(\"" + conf.id + "\",\"" + conf.buildingType + "\")'><i class='fa fa-trash-o' style='font-size:12px;color:red'></i></button></td>";
            tempStr += "</tr>";
            $("#tblConfiguration tbody").append(tempStr);
            delete (_buildingTypes[conf.buildingType]);

            //Set the building type image.
            if (conf.buildingType == "Farm") {
                $(function () {
                    $('#imgFarm').attr('src', '/Images/farm.png');
                });
            } else if (conf.buildingType == "Academy") {
                $(function () {
                    $('#imgAcademy').attr('src', '/Images/academy.png');
                });
            } else if (conf.buildingType == "Headquarters") {
                $(function () {
                    $('#imgHeadquarters').attr('src', '/Images/headquarters.png');
                });
            } else if (conf.buildingType == "LumberMill") {
                $(function () {
                    $('#imgLumberMill').attr('src', '/Images/lumbermill.png');
                });
            } else if (conf.buildingType == "Barracks") {
                $(function () {
                    $('#imgBarracks').attr('src', '/Images/barracks.png');
                });
            }
        });
    });
}

function ResetTable() {
    _configurations = [];
    $("#tblConfiguration tbody tr").remove();
}

function ResetInput() {
    document.getElementById('txtConfId').value = '';
    document.getElementById('txtBuildingCost').value = '';
    document.getElementById('txtConstructionTime').value = '';
}

function CheckValues(confCost,confTime) {
    if (confCost <= 0 || (confTime > 1800 || confTime < 30)) {
        $('#errorText').text("**Building Cost** must be greater than zero and **Construction Time** must be greater than 30 and smaller than 1800 seconds.");
        $('#errorDiv').show();
    } else {
        $('#successText').text("The building successfully saved!");
        $('#successDiv').show();
    }
}

function FillBuildingTypes() {
    $('#cmbBuildingType').empty();
    $.each(_buildingTypes, function (key, value) {
        $('#cmbBuildingType').append(`<option value="${value}">
                                       ${value}
                                  </option>`);
    });
}

function FillArray() {
    _buildingTypes = {};
    _buildingTypes = {
        Farm: "Farm",
        Academy: "Academy",
        Headquarters: "Headquarters",
        LumberMill: "LumberMill",
        Barracks: "Barracks"
    };
}


function Save() {
    var configuration = {
        Id: $.trim($("#txtConfId").val()),
        BuildingType: $.trim($("#cmbBuildingType").val()),
        BuildingCost: $.trim($("#txtBuildingCost").val()),
        ConstructionTime: $.trim($("#txtConstructionTime").val()),
    };
    ResetInput();

    //Check whether building cost and construction time are suitable or not.
    CheckValues(configuration.BuildingCost, configuration.ConstructionTime);
    
    $.post("../Home/SaveConfiguration", configuration, function (conf) {
        LoadConfigurations();
    });
}

function Edit(confId) {
    $('#saveModal').modal('show');
    var conf = _configurations.find(x => x.id == confId);
    $("#txtConfId").val(conf.id);
    $('#cmbBuildingType').append(`<option value="${conf.buildingType}">
                                       ${conf.buildingType}
                                  </option>`);
    $("#txtBuildingCost").val(conf.buildingCost);
    $("#txtConstructionTime").val(conf.constructionTime);
    $('#cmbBuildingType').attr('disabled', 'disabled');
}

function Delete(confId,confType) {
    $.ajax({
        url: "../Home/DeleteConfiguration?confId=" + confId,
        method: 'DELETE'
    })
        .done(function () {
            $('#successText').text("The building deleted successfully!");
            $('#successDiv').show();
            LoadConfigurations();
        });
    location.reload();
}
