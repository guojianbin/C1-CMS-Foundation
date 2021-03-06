var events = require('events');

function InstallLocale() {
  events.EventEmitter.call(this);
}

InstallLocale.prototype.command = function (language) {
	
    this.client.api
		.selectPerspective("System")
		.openTreeNode("Languages")
		.selectTreeNodeAction(language, "Remove Language")
		.clickDialogButton("OK")
		
		.openTreeNode("Languages")
		.assertTreeNodeHasNoChild("Languages", language)
		
	return this.client.api;
};

module.exports = InstallLocale;
