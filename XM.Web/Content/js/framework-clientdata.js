var clients = [];
$(function () {
    clients = $.clientsInit();
})
$.clientsInit = function () {
    var dataJson = {
        menu: [],
        authorizeMenu: [],
        authorizeButton: {},
        role: [],
        type: [],
        agents: []

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
    // 生成菜单导航对象
    var MenuNav = function (allMenu, authorizeMenu) {
        var parentIdArray = [], boo = true;
        for (var i = 0; i < authorizeMenu.length; i++) {
            boo = true;
            var j = 0;
            // 查找父级
            for (j = 0; j < parentIdArray.length; j++) {
                if (authorizeMenu[i].ParentId == parentIdArray[j].Id) {
                    boo = false;
                    break;
                }
            }
            j = 0;
            if (boo) {
                var name = "";
                // 查找父级名称
                for (j = 0; j < allMenu.length; j++) {
                    if (authorizeMenu[i].ParentId == allMenu[j].Id) {
                        name = allMenu[j].Name;
                        break;
                    }
                }
                // 查找父级的子集
                var childNodes = authorizeMenu.filter(function (x, index) {
                    return x.ParentId == authorizeMenu[i].ParentId;
                });
                // 子集单排序
                childNodes.sort(function (before, after) {
                    if (before.SortValue < after.SortValue) {
                        return -1;
                    }
                    else if (before.SortValue > after.SortValue) {
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

        // 请求所有基础信息
        $.ajax({
            url: "/Home/GetCommonData",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.menu = data.Menus;
                dataJson.role = data.Roles;
                dataJson.type = data.Types;
                dataJson.agents = data.Agents;
                var rows = data.Navbars;
                for (var row in rows) {
                    dataJson.authorizeButton["" + rows[row].MenuId] = rows[row];
                }
                MenuNav(dataJson.menu, data.Navbars);
            }
        });

    }
    init();
    return dataJson;
}