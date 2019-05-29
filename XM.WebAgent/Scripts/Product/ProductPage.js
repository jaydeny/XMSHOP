//我们尝试使用VUE进行开发
var VM = new Vue({
    el: "#app",
    data: {
        //全部商品
        Goods: [],
        config: {},
        search: "",
        //代理商品页码
        pageSize: '',
        //显示多少条代理商品数据
        count: '',
        //计算得出代理商品的最后一页
        total: '',
        All_pageSize: '',
        All_count: '',
        All_total: '',
        //全部商品总条数
        GoodsCount: "",
        //代理商品总条数
        myGoodsCount: "",
        //是否显示全部商品
        myGoodsShow: false,
        //我的商品
        myGoods: [],
        fromData: [],
        //更改商品
        editPorduct: [],
        editName: "",
        editIntro: "",
        editPrice: "",
        editStatus: "1"
    },
    created: function () {
        //加载配置文件，将数据填充
        $.ajax({
            url: "../../Content/StaticJson/list.json",
            method: "get"
        }).done((data) => {
            //将配置读取到config里
            this.config = data;
            //获取初始化配置
            const size = data.pageSize;
            const count = data.count;
            //配置页面化
            this.pageSize = size;
            this.count = count;
            this.All_count = count;
            this.All_pageSize = size;
            //请求参数
            const param = {
                "page": size,
                "rows": count
            }
            ////调用加载数据方法，进行获取相关的商品
            this.onloadData(data.goodsUrl.getAllGoods, param);
            //调用我的代理商品方法
            const myParam = {
                "page": size,
                "rows": count
            }
            this.onloadMyGoodsData(data.goodsUrl.myGoods, myParam);
        });
    },
    computed: {

        //计算代理商品的页数
        myGoodsCountFilter() {
            this.total = Math.ceil(this.myGoodsCount / this.count);
            return this.total;
        },
        //计算全部商品的页数
        AllGoodsCountFilter() {
            this.All_total = Math.ceil(this.GoodsCount / this.All_count);
            return this.All_total;
        }

    },
    methods: {
        //加载全部商品数据
        onloadData(url, param) {
            $.ajax({
                url: url,
                method: 'get',
                data: param,
                dataType: 'json'
            }).done((data) => {
                //console.log(data)
                this.Goods = data.rows;
                this.GoodsCount = data.total;
                if (this.Goods.length == 0) {
                    alert("没有数据")
                }
            });
        },
        //加载我的代理商品数据
        onloadMyGoodsData(url, param) {
            //发送请求 ，请求地址 : /Agent/QryAgoods
            $.ajax({
                url: url,
                data: param,
                method: "get",
                dataType: 'json'
            }).then((data) => {
                this.myGoods = data.rows;
                this.myGoodsCount = data.total;
                if (this.myGoods.length == 0) {
                    alert("没有数据")
                }
            });

        },
        //一键发布的动作
        Release(GoodsIndex) {

            //获取当前商品定价
            let price = document.querySelectorAll(".price")[GoodsIndex].value;

            //进行验证
            if (price == '') {

                //定价为空，消息警告，前期使用alert
                alert(this.config.message.warm.priceIsNull);


            } else {
                //获取到当前的商品
                const releaseGoods = this.Goods[GoodsIndex];

                $('#exampleModalCenter').modal('show')
                //设置请求参数
                this.fromData = {
                    "goods_id": releaseGoods.GoodsID,
                    "status_id": 1,
                    "price": price,
                    "goods_name": releaseGoods.GoodsName

                }
                document.querySelectorAll(".price")[GoodsIndex].value = "";
            }
        },
        //进行数值和金钱的转化
        tranfoMoney(num) {
            return this.tranfoMoneyber(num, 2);
        },
        tranfoMoneyber(number, n) {
            if (n != 0) {
                n = (n > 0 && n <= 20) ? n : 2;
            }
            number = parseFloat((number + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
            var sub_val = number.split(".")[0].split("").reverse();
            var sub_xs = number.split(".")[1];

            var show_html = "";
            for (i = 0; i < sub_val.length; i++) {
                show_html += sub_val[i] + ((i + 1) % 3 == 0 && (i + 1) != sub_val.length ? "," : "");
            }

            if (n == 0) {
                return show_html.split("").reverse().join("");
            } else {
                return show_html.split("").reverse().join("") + "." + sub_xs;
            }
        },
        groundSubmir() {
            //发送请求去上架商品 请求地址: /Agent/MakeGoods

            $.ajax({
                url: this.config.goodsUrl.groundUrl,
                data: this.fromData,
                method: "post",
                dataType: 'json'
            }).then((data) => {
                if (data.success) {

                    alert(data.msg);
                } else {
                    alert(data.msg);
                }
            });
            $('#exampleModalCenter').modal('hide');

            this.onloadData(this.config.goodsUrl.getAllGoods);

        },
        //向数据库中发送请求查询数据
        searchAllGoods() {
            //获取查询参数
            let search = this.search;
            //设置请求参数
            const param = {
                "pi": this.pageSize,
                "pageSize": this.count,
                "goods_name": search
            }
            if (this.myGoodsShow) {
                //console.log(true)
                this.onloadData(this.config.goodsUrl.getAllGoods, param);
            } else {
                //console.log(false)
                this.onloadMyGoodsData(this.config.goodsUrl.myGoods, param);
            }

        },
        onMyGoods(even) {
            //console.log(even)
            if (even.target.innerText === this.config.myGoodsBtn) {
                this.myGoodsShow = true;
                event.target.innerText = "全部商品";
            } else {
                this.myGoodsShow = false;
                event.target.innerText = this.config.myGoodsBtn;
            }
        },
        //代理商品分页
        //上一页
        before() {
            const page = this.pageSize;
            if (page <= 1) {
                alert("已经是第一页了")
            } else {
                this.pageSize -= 1;
                this.btn_sub();
            }
        },
        //下一页
        end() {
            const page = this.pageSize;
            if (page >= this.total) {
                alert("已经是最后一页了")
            } else {
                this.pageSize += 1;
                this.btn_sub();
            }
        },
        //提交页
        btn_sub() {
            console.log(this.pageSize)
            const param = {
                "page": this.pageSize,
                "rows": this.count
            }
            this.onloadMyGoodsData(this.config.goodsUrl.myGoods, param);
        },

        //全部商品分页
        //上一页
        All_before() {
            const page = this.All_pageSize;
            if (page <= 1) {
                alert("已经是第一页了")
            } else {
                this.All_pageSize -= 1;
                this.All_btn_sub();
            }
        },
        //下一页
        All_end() {
            const page = this.All_pageSize;
            if (page >= this.All_total) {
                alert("已经是最后一页了")
            } else {
                this.All_pageSize += 1;
                this.All_btn_sub();
            }
        },
        //提交页
        All_btn_sub() {
            console.log(this.All_pageSize)
            console.log(this.All_count)
            const param = {
                "page": this.All_pageSize,
                "rows": this.All_count
            }
            this.onloadData(this.config.goodsUrl.getAllGoods, param);
        },

        //修改代理商品信息
        AlertAgentGoods(index) {
            //获取对应的代理商品信息
            const product = this.myGoods[index];
            //存储对应代理信息，方便后面进行修改的读取
            this.editPorduct = product;
            this.editName = product.goods_name;
            this.editIntro = product.goods_intro;
            this.editPrice = product.price;
            this.editStatus = product.status_id;
            $('#AlertAgentGoods').modal('show');
        },
        Goods_Alert_Submir() {

            //定义参数集
            const param = {
                "Agoods_id": this.editPorduct.id,
                "goods_id": this.editPorduct.goods_id,
                "goods_name": this.editName,
                "price": this.editPrice,
                "status_id": this.editStatus,
                "goods_intro": this.editIntro
            }
            //发生请求
            $.ajax({
                url: "/Product/MakeGoods",
                data: param,
                dataType: "json"
            }).then((data) => {
                if (data.success) {
                    alert(data.msg);
                } else {
                    alert(data.msg)
                }
                const param = {
                    "page": this.pageSize,
                    "rows": this.count
                }
                this.onloadMyGoodsData(this.config.goodsUrl.myGoods, param);

            })
            $('#AlertAgentGoods').modal('hide');
        }
    }
});