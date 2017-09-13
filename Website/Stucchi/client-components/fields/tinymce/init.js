tinymce.init({
    selector: 'textarea',
    height: "350",
    extended_valid_elements: 'span',
    content_css: "/client-components/fields/tinymce/styles.css",
    theme: 'modern',
    extended_valid_elements: 'span',
    plugins: [
      'advlist autolink lists link image charmap print preview hr anchor pagebreak',
      'searchreplace wordcount visualblocks visualchars code fullscreen',
      'insertdatetime media nonbreaking save table contextmenu directionality',
      'emoticons paste textcolor colorpicker textpattern imagetools codesample toc help'
    ],
    toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
    toolbar2: 'print preview media | forecolor backcolor emoticons | codesample help',
    image_advtab: true,
    style_formats: [
        {
            title: 'Headers', items: [
            { title: 'h1', block: 'h1' },
            { title: 'h2', block: 'h2' },
            { title: 'h3', block: 'h3' },
            { title: 'h4', block: 'h4' },
            { title: 'h5', block: 'h5' },
            { title: 'h6', block: 'h6' }
            ]
        },

        {
            title: 'Blocks', items: [
            { title: 'p', block: 'p' },
            { title: 'div', block: 'div' },
            { title: 'span', block: 'span' }
            ]
        },

        { title: 'Pink text', inline: 'span', styles: { color: 'inherit' } }
    ]
});