(function () {
    function getRoundedCanvas(sourceCanvas) {
        var canvas = document.createElement('canvas');
        var context = canvas.getContext('2d');
        var width = sourceCanvas.width;
        var height = sourceCanvas.height;

        canvas.width = width;
        canvas.height = height;

        context.imageSmoothingEnabled = true;
        context.drawImage(sourceCanvas, 0, 0, width, height);
        context.globalCompositeOperation = 'destination-in';
        context.beginPath();
        context.arc(width / 2, height / 2, Math.min(width, height) / 2, 0, 2 * Math.PI, true);
        context.fill();

        return canvas;
    }

    window.addEventListener('DOMContentLoaded', function () {
        var image = document.getElementById('image');
        var button = document.getElementById('button');
        var result = document.getElementById('result');
        var croppable = false;
        var cropper = new Cropper(image, {
            aspectRatio: 1,
            viewMode: 1,
            ready: function () {
                croppable = true;
            }
        });

        button.onclick = function () {
            var croppedCanvas, roundedCanvas, roundedImage;

            if (!croppable) {
                return;
            }

            // Crop
            croppedCanvas = cropper.getCroppedCanvas();

            // Round
            roundedCanvas = getRoundedCanvas(croppedCanvas);

            // Show
            roundedImage = document.createElement('img');
            roundedImage.src = roundedCanvas.toDataURL();
            result.innerHTML = '';
            result.appendChild(roundedImage);
        };

    });

})();

//做个下简易的验证  大小 格式 
$('#avatarInput').on('change', function (e) {
    var filemaxsize = 1024 * 5;//5M
    var target = $(e.target);
    var size = target[0].files[0].size / 1024;
    if (size > filemaxsize) {
        DialogTipsMsgError("'图片过大，请重新选择!", 1000);
        $(".avatar-wrapper").childre().remove();
        return false;
    }
    if (!this.files[0].type.match(/image.*/)) {
        alert('请选择正确的图片!');
    } else {
        var filename = document.querySelector("#avatar-name");
        var texts = document.querySelector("#avatarInput").value;
        var teststr = texts; //你这里的路径写错了
        var testend = teststr.match(/[^\\]+\.[^\(]+/i); //直接完整文件名的
        filename.innerHTML = testend;
    }
});

$("#headImageSubmit").on("click",
    function () {
        debugger;
        var imgLg = document.getElementById('imageHead');
        // 截图小的显示框内的内容
        html2canvas(imgLg, 
            {
                allowTaint: true,
                taintTest: false,
                onrendered: function (canvas) {
                    canvas.id = "mycanvas";
                    //生成base64图片数据
                    var dataUrl = canvas.toDataURL("image/jpeg");
                    var newImg = document.createElement("img");
                    newImg.src = dataUrl;
                    imagesAjax(dataUrl);
                }
            });
    });

//提交
function imagesAjax(src) {
    UtilAjaxPostWait("User/SaveHeadImage", { id: src }, function (data) {
        DialogAjaxResult(data);
        if (data.ResultSign === 0) {
            $(".img-circle").attr('src', src);
            //修改localStorage
            var storage = window.localStorage;
            storage["HeadImage"] = src;
            dialog.close();
        }
    });
}
