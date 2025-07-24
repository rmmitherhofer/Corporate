document.addEventListener('DOMContentLoaded', () => {
    // Accordion functionality
    const accordionHeaders = document.querySelectorAll('.card-header');

    accordionHeaders.forEach(header => {
        header.addEventListener('click', () => {
            const card = header.parentElement;
            const content = card.querySelector('.card-content');
            const icon = header.querySelector('.card-header-icon i');

            if (content.style.display === 'none') {
                content.style.display = 'block';
                icon.classList.remove('fa-angle-up');
                icon.classList.add('fa-angle-down');
            } else {
                content.style.display = 'none';
                icon.classList.remove('fa-angle-down');
                icon.classList.add('fa-angle-up');
            }
        });
    });

    // Add event listeners to existing remove buttons
    document.querySelectorAll('.remove-block').forEach(button => {
        button.addEventListener('click', function () {
            const sectionToReindex = this.closest('section');

            this.parentElement.remove();

            reindexDynamicSection(sectionToReindex);
        });
    });

    function reindexDynamicSection(section) {
        const blocks = section.querySelectorAll('.dynamic-block');

        blocks.forEach((block, index) => {
            block.querySelectorAll('[name]').forEach(input => {
                const oldName = input.getAttribute('name');
                if (!oldName) return;

                // Substitui o índice antigo pelo novo, preservando o prefixo do grupo (ex: Formacoes[2].Descricao)
                const newName = oldName.replace(/\[\d+\]/, `[${index}]`);
                input.setAttribute('name', newName);

                // Opcional: atualizar id
                if (input.id) {
                    input.id = newName.replace(/[\[\]\.]/g, '_');
                }
            });
        });
    }
});