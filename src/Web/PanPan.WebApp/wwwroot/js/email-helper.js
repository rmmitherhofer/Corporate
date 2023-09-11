var domains = ["yahoo.com", "gmail.com", "google.com", "hotmail.com", "me.com", "aol.com", "mac.com", "live.com", "comcast.com", "googlemail.com", "msn.com", "hotmail.co.uk", "yahoo.co.uk", "facebook.com", "verizon.net", "att.net", "gmz.com", "mail.com"];

// Using Low Yong Zhen's jQuery plugin
var $pluginEmailElem = $('.email-helper').emailautocomplete({
    suggClass: 'email-autocomplete',
    labelClass: 'email-label',
    domains: domains
});