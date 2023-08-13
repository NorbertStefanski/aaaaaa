package be.pxl.services;


import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;

/**
 * Discovery Service
 *
 */
@SpringBootApplication
@EnableEurekaServer //Maakt van deze springboot applicatie een discovery service
public class DiscoveryServiceApplication
{
    public static void main( String[] args )

    {
        SpringApplication.run(DiscoveryServiceApplication.class, args);
    }
}
