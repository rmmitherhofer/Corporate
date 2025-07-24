document.addEventListener('DOMContentLoaded', () => {
    const copyBtn = document.getElementById('copy-btn');
    copyBtn.addEventListener('click', () => {
        const copySection = document.getElementById('copy-area');
        const textToCopy = copySection.innerText.trim();

        navigator.clipboard.writeText(textToCopy)
            .then(() => {
                copyBtn.innerHTML = '<i class="fas fa-check"></i> Copiado!';
                setTimeout(() => {
                    copyBtn.innerHTML = '<i class="fas fa-copy"></i> Copiar Instruções';
                }, 2000);
            })
            .catch(err => {
                console.error('Erro ao copiar: ', err);
            });
    });

    copyBtn.disabled = true;
    setTimeout(() => {
        copyBtn.innerHTML = '<i class="fas fa-copy"></i> Copiar Instruções';
        copyBtn.disabled = false;
    }, 2000);

    document.querySelectorAll('div.code').forEach(el => {
        hljs.highlightElement(el);
    });
});