describe('template spec', () => {
  it('passes', () => {

    cy.visit('http://localhost:8080')
      Cypress.on('uncaught:exception', (err, runnable) => {
        return false;
    });


    cy.get('#inputEmailLogin').type('admin');
    cy.get('#inputPasswordLogin').type('123');

    cy.get('#botaoLogin').click();
    cy.wait(2000)

    // cy.get('#controlPaginas').click();
    // cy.get('#paginaEditar1').click()
    // cy.get('#paginaEditar1471').click();

    // cy.get('#paginaContatoUp-5374').click()
    // cy.wait(2000)

    // cy.get('#paginaContatoDown-5374').click()
    // cy.get('#site-6981 > .col-md-2 > .btn').click()

    // cy.get('#contato-5369 > .col-md-2 > .btn').click()

    // cy.get('#paginaSiteUp-6998').click()
  })
})