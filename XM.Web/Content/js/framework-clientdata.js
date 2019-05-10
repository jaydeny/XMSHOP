var clients = [];
$(function () {
    clients = $.clientsInit();
})
$.clientsInit = function () {
    var dataJson = {
        //dataItems: [],
        //organize: [],
        //role: [],
        //duty: [],
        //user: [],
        //authorizeMenu: [],
        //authorizeButton: []
        menu: [],
        authorizeMenu:[]
    };
    // 导航对象
    var MenuObj = function (id, name, urlAddress, sort, childNodes) {
        return {
            Id: id,
            Name: name,
            UrlAddress: urlAddress,
            Sort: sort,
            ChildNodes: childNodes
        }
    };
    var MenuNav = function (allMenu, authorizeMenu) {
        var parentIdArray = [], boo = true;
        for (var i = 0; i < authorizeMenu.length; i++) {
            boo = true;
            var j = 0;
            for (j = 0; j < parentIdArray.length; j++) {
                if (authorizeMenu[i].ParentId == parentIdArray[j].Id) {
                    boo = false;
                    break;
                }
            }
            j = 0;
            if (boo) {
                var name = "";
                for (j = 0; j < allMenu.length; j++) {
                    if (authorizeMenu[i].ParentId == allMenu[j].Id) {
                        name = allMenu[j].Name;
                        break;
                    }
                }
                // 获取菜单子菜单
                var childNodes = allMenu.filter(function (x, index) {
                    return x.ParentId == authorizeMenu[i].ParentId;
                });
                // 子菜单排序
                childNodes.sort(function (before, after) {
                    if (before < after) {
                        return -1;
                    }
                    else if (before > after) {
                        return 1;
                    }
                    return 0;
                });
                parentIdArray.push(MenuObj(authorizeMenu[i].ParentId, name, "", authorizeMenu[i].SortValue, childNodes));

            }
        }
        dataJson.authorizeMenu = parentIdArray;

    };
    var init = function () {
        //$.ajax({
        //    url: "/ClientsData/GetClientsDataJson",
        //    type: "get",
        //    dataType: "json",
        //    async: false,
        //    success: function (data) {
        //        dataJson.dataItems = data.dataItems;
        //        dataJson.organize = data.organize;
        //        dataJson.role = data.role;
        //        dataJson.duty = data.duty;
        //        dataJson.authorizeMenu = eval(data.authorizeMenu);
        //        dataJson.authorizeButton = data.authorizeButton;
        //    }
        //});


        // 请求所有菜单
        $.ajax({
            url: "/Menu/GetAllMenu",
            type: "get",
            dataType: "json",
            async: false,
            data: { rows:100 },
            success: function (data) {
                dataJson.menu = data.rows;
            }
        });
        // 授权菜单
        $.ajax({
            url: "/Home/LoadMenu",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                MenuNav(dataJson.menu, data.rows);
            }
        });

    }
    init();
    return dataJson;
}