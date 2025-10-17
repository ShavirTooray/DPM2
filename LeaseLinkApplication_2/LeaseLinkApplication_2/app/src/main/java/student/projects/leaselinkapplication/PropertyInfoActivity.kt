package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class PropertyInfoActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_property_info)

        // Get references to views
        val propertyNameText = findViewById<TextView>(R.id.textView36)
        val locationText = findViewById<TextView>(R.id.textView40)
        val priceText = findViewById<TextView>(R.id.textView43)
        val bedsBathsText = findViewById<TextView>(R.id.textView44)
        val descriptionText = findViewById<TextView>(R.id.textView45)
        val propertyImage = findViewById<ImageView>(R.id.imageView12)
        val applyButton = findViewById<Button>(R.id.button7)

        // Get property data from intent
        val propertyName = intent.getStringExtra("propertyName")
        val location = intent.getStringExtra("location")
        val price = intent.getStringExtra("price")
        val bedrooms = intent.getIntExtra("bedrooms", 0)
        val bathrooms = intent.getIntExtra("bathrooms", 0)
        val description = intent.getStringExtra("description")
        val imageResId = intent.getIntExtra("imageResId", R.drawable.istockphoto_109350275_612x612)

        // Set the data into the views
        propertyNameText.text = propertyName
        locationText.text = "Location: $location"
        priceText.text = "Price: $price / month"
        bedsBathsText.text = "Bedrooms: $bedrooms  Bathrooms: $bathrooms"
        descriptionText.text = "Description: $description"
        propertyImage.setImageResource(imageResId)

        applyButton.setOnClickListener {
            val intent = Intent(this, ApplicationActivity::class.java)
            startActivity(intent)
        }
    }
}


