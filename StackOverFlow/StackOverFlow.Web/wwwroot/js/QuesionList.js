$(function () {
    $("#questions").DataTable({
        "responsive": true,
        "lengthChange": false,
        "autoWidth": false,
        "columnDefs": [
            {
                "orderable": false,
                "targets": 0,
                "render": function (data, type, row) {
                    return `<span class="s-badge s-badge__votes">${data} Votes</span>`;
                }
            },
            {
                "orderable": false,
                "targets": 1,
                "render": function (data, type, row) {
                    if (data >= 4) {
                        return `<span class="s-badge s-badge__important">${data}+ Answers</span>`;
                    }
                    else if (data < 4) {
                        return `<span class="s-badge s-badge__answered">${data} Answers</span>`;
                    }
                }
            },
            {
                "orderable": false,
                "targets": 2,
                "render": function (data, type, row) {
                    return `<span class="s-post-summary--stats-item-unit">
                        ${data} views
                    </span>`;
                }
            },
            {
                "orderable": false,
                "targets": 3,
                "render": function (data, type, row) {
                    return `<h3 class="s-post-summary--content-title">
                        ${data}
                    </h3>`;
                }
            },
            {
                "orderable": false,
                "targets": 4,
                "render": function (data, type, row) {
                    return renderTags(data); 
                }
            },
            {
                "orderable": false,
                "targets": 5,
                "render": function (data, type, row) {
                    return `<button type="submit" class="s-btn s-btn__outlined" onclick="window.location.href='/user/questions/Details/${data}'"  value='${data}'>See Details
                                  <svg width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" clip-rule="evenodd" d="M5 3C3.89543 3 3 3.89543 3 5V15C3 16.1046 3.89543 17 5 17H12C13.1046 17 14 16.1046 14 15V5C14 3.89543 13.1046 3 12 3H5ZM7 6C7 6.55228 6.55228 7 6 7C5.44772 7 5 6.55228 5 6C5 5.44772 5.44772 5 6 5C6.55228 5 7 5.44772 7 6ZM5 10.5C5 10.2239 5.22386 10 5.5 10H11.5C11.7761 10 12 10.2239 12 10.5C12 10.7761 11.7761 11 11.5 11H5.5C5.22386 11 5 10.7761 5 10.5ZM5.5 12H11.5C11.7761 12 12 12.2239 12 12.5C12 12.7761 11.7761 13 11.5 13H5.5C5.22386 13 5 12.7761 5 12.5C5 12.2239 5.22386 12 5.5 12ZM5 14.5C5 14.2239 5.22386 14 5.5 14H11.5C11.7761 14 12 14.2239 12 14.5C12 14.7761 11.7761 15 11.5 15H5.5C5.22386 15 5 14.7761 5 14.5Z" fill="black"/>
                        <path opacity="0.4" d="M5.90479 2H12.25C13.7688 2 15 3.23122 15 4.75V14.0952C15.6163 13.5037 16 12.6717 16 11.75V4.25C16 2.45507 14.5449 1 12.75 1H8.25C7.32832 1 6.49625 1.38366 5.90479 2Z" fill="black"/>
                        </svg> 
                            </button>`;
                }
            }
        ],
        "sort": false,
        "searching": false,
        "processing": true,
        "serverSide": true,
        "ajax": {
            url: "/User/Questions/GetQuestions",
            type: "POST",
        }
    });




    function renderTags(tags) {
        if (!tags || tags.length === 0) return "";

        var result = ""; 

        for (var i = 0; i < tags.length; i++) {
            result += '<a href="" class="s-tag">' + tags[i] + '</a>';
            console.log(tags[i]);

    
            if (i < tags.length - 1) {
                result += '  ';
            }
        }

        return result; 
    }
});
